using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

namespace Airfare.Models
{
    public class BaseModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ConcurrentDictionary<string, List<string>> _propertyErrors = new ConcurrentDictionary<string, List<string>>();

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _propertyErrors.Any();

        public void AddError(string propertyName, string errorMessage)
        {
            _propertyErrors.GetOrAdd(propertyName, _ => new List<string>()).Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.TryRemove(propertyName, out _))
            {
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName) =>
            _propertyErrors.TryGetValue(propertyName, out var errors) ? errors : null;

        public IEnumerable<string> GetAllErrors()
        {
            return _propertyErrors.Values.SelectMany(x => x);
        }
    }

}
