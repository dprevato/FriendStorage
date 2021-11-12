﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException

namespace FriendStorage.UI.Wrapper;

public class ModelWrapper<T> : Observable, IRevertibleChangeTracking
{
    private readonly Dictionary<string, object> _originalValues; // Serve per il Change Tracking
    private readonly List<IRevertibleChangeTracking> _trackingObjects;

    public T Model { get; }

    public ModelWrapper(T model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        Model = model;
        _originalValues = new Dictionary<string, object>();
        _trackingObjects = new List<IRevertibleChangeTracking>();
    }


    #region Implementation of IRevertibleChangeTracking
    public bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(x => x.IsChanged); // basta che ci sia un solo elemento per rendere Changed == false

    public void AcceptChanges()
    {
        _originalValues.Clear(); // Questo deve essere lanciato soltanto **DOPO** aver salvato i nuovi valori
        foreach (var item in _trackingObjects)
        {
            item.AcceptChanges();
        }
        OnPropertyChanged(string.Empty);  // Questo chiama RaisePropertyChanged per tutte le property della classe
    }

    public void RejectChanges()
    {
        foreach (var entry in _originalValues)
        {
            typeof(T).GetProperty(entry.Key).SetValue(Model, entry.Value);
        }
        _originalValues.Clear();
        foreach (var item in _trackingObjects)
        {
            item.RejectChanges();
        }
        OnPropertyChanged(string.Empty);  // Questo chiama RaisePropertyChanged per tutte le property della classe
    }

    #endregion


    #region Methods

    // todo: Spostare PropertyGet e PropertySet nella classe BindableBase
    protected TVal GetProperty<TVal>([CallerMemberName] string propertyName = null)
    {
        var propertyInfo = Model.GetType().GetProperty(propertyName);
        return (TVal)propertyInfo.GetValue(Model);
    }

    // TODO: Verificare se questa funzione fa sì che venga chiamata due volte PropertyGet
    protected TVal GetOriginalValue<TVal>([CallerMemberName] string propertyName = null) => _originalValues.ContainsKey(propertyName) ? (TVal)_originalValues[propertyName] : GetProperty<TVal>(propertyName);

    // GetChanged e GetOriginalValue sono i due metodi che mi servono per implementare il change tracking.
    // La logica di GetChanged consiste nel verificare se il dictionary contiene tra le chiavi la property specificata; se c'è, significa che
    // la property è cambiata rispetto al valore originale. Quando per qualsiasi motivo il valore corrente si trova ad essere uguale a quello
    // originale, l'entry viene rimossa dal dictionary.
    protected bool GetIsChanged([CallerMemberName] string propertyName = null) => _originalValues.ContainsKey(propertyName);

    protected bool SetProperty<T1>(T1 newValue, [CallerMemberName] string propertyName = null)
    {
        var propertyInfo = Model.GetType().GetProperty(propertyName);
        var currentValue = propertyInfo.GetValue(Model);
        if (Equals(currentValue, newValue)) return false;

        // La linea seguente contiene codice per aggiornare il valore originale della property - parte del progetto ChangeTracking
        UpdateOriginalValue(currentValue, newValue, propertyName);
        propertyInfo.SetValue(Model, newValue); // Questo è il SetValue di Reflection!
        OnPropertyChanged(propertyName);
        OnPropertyChanged($"{propertyName}IsChanged");
        return true;
    }

    protected bool SetProperty<T1>(T1 newValue, Action onChanged, [CallerMemberName] string propertyName = null)
    {
        var propertyInfo = Model.GetType().GetProperty(propertyName);
        var currentValue = propertyInfo.GetValue(Model);
        if (Equals(currentValue, newValue)) return false;

        // La linea seguente contiene codice per aggiornare il valore originale della property - parte del progetto ChangeTracking
        UpdateOriginalValue(currentValue, newValue, propertyName);
        propertyInfo.SetValue(Model, newValue); // Questo è il SetValue di Reflection!
        OnPropertyChanged(propertyName);
        OnPropertyChanged($"{propertyName}IsChanged");
        onChanged?.Invoke();
        return true;
    }

    private void UpdateOriginalValue(object currentValue, object newValue, string propertyName)
    {
        if (!Equals(currentValue, newValue) && !_originalValues.ContainsKey(propertyName))
        {
            _originalValues.Add(propertyName, currentValue);
            OnPropertyChanged(nameof(IsChanged));
        }
        else
        {
            if (Equals(_originalValues[propertyName], newValue))
            {
                _originalValues.Remove(propertyName);
                OnPropertyChanged(nameof(IsChanged));
            }
        }
    }

    protected void RegisterCollection<TWrapper, TModel>(ObservableCollection<TWrapper> wrapperCollection, List<TModel> modelCollection) where TWrapper : ModelWrapper<TModel>
    {
        wrapperCollection.CollectionChanged += ((s, e) =>
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.Cast<TWrapper>())
                {
                    modelCollection.Remove(item.Model);
                }
            }

            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.Cast<TWrapper>())
                {
                    modelCollection.Add(item.Model);
                }
            }
        });
    }

    protected void RegisterComplex<TModel>(ModelWrapper<TModel> wrapper)
    {
        if (!_trackingObjects.Contains(wrapper))
        {
            _trackingObjects.Add(wrapper);
            wrapper.PropertyChanged += TrackingObjectPropertyChanged;
        }
    }

    private void TrackingObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsChanged))
        {
            OnPropertyChanged(nameof(IsChanged));
        }
    }

    #endregion Methods
}