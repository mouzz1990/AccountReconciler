using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AccountReconcilerLibrary.ViewModels
{
    public class RulesViewModel : INotifyPropertyChanged
    {
        DatabaseContext context;
        Messager messager;
        DialogManager dialogManager;

        public RulesViewModel()
        {
            context = DatabaseManager.DatabaseContext;
            messager = new Messager();
            dialogManager = new DialogManager();

            context.Groups1.ToList();
            context.Accounts.ToList();
            context.Rules.ToList();
            context.RuleAccounts.ToList();

            RuleAccounts = new ObservableCollection<Account>();
            RuleContains = new ObservableCollection<DetailsControlReturned>();
            RulesForSelectedGroup = new ObservableCollection<Rule>();

            Groups1 = context.Groups1.Local;
            Accounts = context.Accounts.Local;
            RuleOperatorsList = new List<string>() { "Less than", "Equals to", "More than" };

            //for editing
            this.PropertyChanged += RulesViewModel_PropertyChanged;

            IsAnyValueChecked = true;
        }

        #region Properties
        private ObservableCollection<Group1> groups1;
        public ObservableCollection<Group1> Groups1
        {
            get { return groups1; }
            set { groups1 = value; }
        }

        private ObservableCollection<Account> accounts;
        public ObservableCollection<Account> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }
        
        private Object selectedGroupRules;
        public Object SelectedGroupRules
        {
            get { return selectedGroupRules; }
            set { selectedGroupRules = value; OnPropertyChanged("SelectedGroupRules"); }
        }

        private ObservableCollection<Account> ruleAccounts;
        public ObservableCollection<Account> RuleAccounts
        {
            get { return ruleAccounts; }
            set { ruleAccounts = value; }
        }

        private ObservableCollection<DetailsControlReturned> ruleContains;
        public ObservableCollection<DetailsControlReturned> RuleContains
        {
            get { return ruleContains; }
            set { ruleContains = value; }
        }

        private bool isAnyValueChecked;
        public bool IsAnyValueChecked
        {
            get { return isAnyValueChecked; }
            set { isAnyValueChecked = value; OnPropertyChanged("IsAnyValueChecked"); }
        }

        private string ruleOperator;
        public string RuleOperator
        {
            get { return ruleOperator; }
            set { ruleOperator = value; OnPropertyChanged("RuleContains"); }
        }

        private double? ruleValue;
        public double? RuleValue
        {
            get { return ruleValue; }
            set { ruleValue = value; OnPropertyChanged("RuleValue"); }
        }

        private string ruleComment;
        public string RuleComment
        {
            get { return ruleComment; }
            set { ruleComment = value; OnPropertyChanged("RuleComment"); }
        }

        private List<string> ruleOperatorsList;
        public List<string> RuleOperatorsList
        {
            get { return ruleOperatorsList; }
            set { ruleOperatorsList = value; }
        }
        #endregion

        #region Commands
        private RCommand ruleSave;
        public RCommand RuleSave
        {
            get
            {
                return ruleSave ?? (ruleSave = new RCommand(
                    (obj) =>
                    {
                        Rule rule = new Rule();

                        if (SelectedGroupRules is Group1)
                            rule.Group1 = (Group1)SelectedGroupRules;

                        if (SelectedGroupRules is Group2)
                        {
                            rule.Group1 = ((Group2)SelectedGroupRules).Group1;
                            rule.Group2 = (Group2)SelectedGroupRules;
                        }

                        rule.RuleAccounts = new ObservableCollection<RuleAccount>();
                        foreach (var ra in RuleAccounts)
                        {
                            rule.RuleAccounts.Add(new RuleAccount() { RuleAccountAttached = ra});
                        }

                        //rule.RuleAccounts = RuleAccounts;

                        rule.RuleContainsStrings = RuleContains;

                        if (!IsAnyValueChecked)
                        {
                            rule.RuleOperator = RuleOperator;
                            rule.RuleValue = (double)RuleValue;
                        }

                        if (!string.IsNullOrEmpty(RuleComment))
                            rule.RuleComment = RuleComment;

                        context.Rules.Add(rule);
                        context.SaveChanges();

                        messager.SuccessMessage("Rule successfully created!");

                        NavigationManager.GoToReconcile();
                    },
                    (obj) => 
                    {
                        if (RuleAccounts == null || RuleContains == null) return false;
                        return (RuleAccounts.Count >0 && RuleContains.Count > 0 && SelectedGroupRules != null);
                    }
                    ));
            }
        }

        private RCommand goBackCommand;
        public RCommand GoBackCommand
        {
            get
            {
                return goBackCommand ?? (goBackCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoBack();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand goToAddGroupCommand;
        public RCommand GoToAddGroupCommand
        {
            get
            {
                return goToAddGroupCommand ?? (goToAddGroupCommand = new RCommand(
                    (obj) =>
                    {
                        if (SelectedGroupRules is Group1)
                            NavigationManager.GoToNewGroup2((Group1)SelectedGroupRules);

                        if (SelectedGroupRules is Group2)
                            NavigationManager.GoToNewGroup2(((Group2)SelectedGroupRules).Group1);
                    },
                    (obj) => { return SelectedGroupRules != null; }
                    ));
            }
        }

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
                            context.Rules.Remove(SelecterRule);
                            RulesForSelectedGroup.Remove(SelecterRule);
                            context.SaveChanges();
                        }
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand changeRuleCommand;
        public RCommand ChangeRuleCommand
        {
            get
            {
                return changeRuleCommand ?? (changeRuleCommand = new RCommand(
                    (obj) =>
                    {
                        context.SaveChanges();
                        messager.SuccessMessage("Changes successfully saved!");
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        
        private RCommand addAccountCommand;
        public RCommand AddAccountCommand
        {
            get
            {
                return addAccountCommand ?? (addAccountCommand = new RCommand(
                    (obj) =>
                    {
                        SelecterRule.RuleAccounts.Add(new RuleAccount());
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand removeAccountCommand;
        public RCommand RemoveAccountCommand
        {
            get
            {
                return removeAccountCommand ?? (removeAccountCommand = new RCommand(
                    (obj) =>
                    {
                        try
                        {
                            SelecterRule.RuleAccounts.Remove(SelectedEditingRuleAccount);
                        }
                        catch (Exception ex)
                        {
                            messager.ErrorMessage(
                                string.Format("There are some problem, please contact with programmer.{0}Error message:{0}{1}{0}RulesViewModel.RemoveAccountCommand",
                                Environment.NewLine, ex.Message));
                        }

                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand removeContainsString;
        public RCommand RemoveContainsString
        {
            get
            {
                return removeContainsString ?? (removeContainsString = new RCommand(
                    (obj) =>
                    {
                        SelecterRule.RuleContainsStrings.Remove(SelectedEditDetailsResult);
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand addContainsString;
        public RCommand AddContainsString
        {
            get
            {
                return addContainsString ?? (addContainsString = new RCommand(
                    (obj) =>
                    {
                        SelecterRule.RuleContainsStrings.Add(new DetailsControlReturned());
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        #endregion

        #region Methods for editing rules
        private ObservableCollection<Rule> rulesForSelectedGroup;
        public ObservableCollection<Rule> RulesForSelectedGroup
        {
            get { return rulesForSelectedGroup; }
            set { rulesForSelectedGroup = value; }
        }

        private Rule selecterRule;
        public Rule SelecterRule
        {
            get { return selecterRule; }
            set { selecterRule = value; OnPropertyChanged("SelecterRule"); }
        }

        private RuleAccount selectedEditingRuleAccount;
        public RuleAccount SelectedEditingRuleAccount
        {
            get { return selectedEditingRuleAccount; }
            set { selectedEditingRuleAccount = value; OnPropertyChanged("SelectedEditingRuleAccount"); }
        }


        private DetailsControlReturned selectedEditDetailsResult;
        public DetailsControlReturned SelectedEditDetailsResult
        {
            get { return selectedEditDetailsResult; }
            set { selectedEditDetailsResult = value; OnPropertyChanged("SelectedEditDetailsResult"); }
        }


        private void RulesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedGroupRules")
            {
                RulesForSelectedGroup.Clear();

                Group1 selGr1 = SelectedGroupRules as Group1;
                if (selGr1 != null)
                {
                    var rg = context.Rules.Where(x => x.Group1.Group1ID == selGr1.Group1ID).ToList();
                    foreach (var r in rg)
                        RulesForSelectedGroup.Add(r);

                    Debug.WriteLine(RulesForSelectedGroup.Count);
                    return;
                }

                Group2 selGr2 = SelectedGroupRules as Group2;
                if (selGr2 != null)
                {
                    var rg = context.Rules.Where(x => x.Group2.Group2ID== selGr2.Group2ID).ToList();
                    foreach (var r in rg)
                        RulesForSelectedGroup.Add(r);

                    Debug.WriteLine(RulesForSelectedGroup.Count);
                    return;
                }
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
