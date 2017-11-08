using AccountReconcilerLibrary;
using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.ImportExportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconciler.ViewModels
{
    public class ExportCsvViewModel
    {
        ImportExportCsvManager exportManager;
        DialogManager dialogManager;
        Messager messager;

        public ExportCsvViewModel()
        {
            exportManager = new ImportExportCsvManager();
            dialogManager = new DialogManager();
            messager = new Messager();
        }

        private RCommand exportCommand;
        public RCommand ExportCommand
        {
            get
            {
                return exportCommand ?? (exportCommand = new RCommand(
                    (obj) => 
                    {
                        try
                        {
                            string pathToSave = dialogManager.SaveFile();

                            if (string.IsNullOrEmpty(pathToSave)) return;

                            exportManager.ExportCsvFile(pathToSave);
                            messager.SuccessMessage("Successfully Saved!");

                        }
                        catch (Exception e)
                        {
                            messager.ErrorMessage(e.Message);
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
    }
}
