using Airfare.Commands;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.ViewModels.DialogViewModels
{
    public class YesNoDialogViewModel : BaseViewModel, IDialogResultable<bool>,IDisposable
    {
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private bool Answer;

        bool IDialogResultable<bool>.Result
        {
            get { return Answer; }
            set { Answer = value; }
        }

        Action close;

        Action IDialogResultable<bool>.CloseAction
        {
            get { return close; }
            set { close = value; }
        }

        public RelayCommand YesAnswerCommand { get; set; }
        public RelayCommand NoAnswerCommand { get; set; }

        public YesNoDialogViewModel()
        {
            YesAnswerCommand = new RelayCommand(YesAnswer);
            NoAnswerCommand = new RelayCommand(NoAnswer);
        }

        private void NoAnswer()
        {
            Answer = false;
            close.Invoke();
        }

        private void YesAnswer()
        {
            Answer = true;
            close.Invoke();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
