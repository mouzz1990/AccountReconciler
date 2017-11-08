using AccountReconcilerLibrary;
using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconciler.ViewModels
{
    public class ModifyRulesViewModel : INotifyPropertyChanged
    {
        DatabaseContext context;
        DialogManager dialogManager;
        Messager messager;

        public ModifyRulesViewModel()
        {
            context = DatabaseManager.DatabaseContext;
            dialogManager = new DialogManager();
            messager = new Messager();
            //context.Groups1.ToList();
            //context.Groups2.ToList();
            context.Rules.ToList();

            Rules = context.Rules.Local;
        }

        #region Properties
        private ObservableCollection<Rule> rules;
        public ObservableCollection<Rule> Rules
        {
            get { return rules; }
            set { rules = value; }
        }

        private Rule selecterRule;
        public Rule SelecterRule
        {
            get { return selecterRule; }
            set { selecterRule = value; OnPropertyChanged("SelecterRule"); }
        }

        #endregion

        #region Commands

        private RCommand removeRuleCommand;
        public RCommand RemoveRuleCommand
        {
            get
            {
                return removeRuleCommand ?? (removeRuleCommand = new RCommand(
                    (obj) => 
                    {
                        if (dialogManager.QuestionMessage("Are you realy want to delete selected Rule?", "Delete Rule?"))
                        {
                            Rules.Remove(SelecterRule);
                            context.SaveChanges();
                        }
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand goBackCommand;
        public RCommand GoBackCommand
        {
            get
            {
                return goBackCommand ?? (goBackCommand = new RCommand(
                    (obj) => { NavigationManager.GoBack(); },
                    (obj) => { return true; }
                    ));
            }
        }

        #endregion

        #region INotifyPropertyChanged implemented interface
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

    }
}
