using System.Windows;

namespace DynamicDllLoader
{
    class Main : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {

            Window taskSearchWindow = new Window();
            LoaderFormWPF taskview = new LoaderFormWPF();

            taskSearchWindow.Width = 320;
            taskSearchWindow.Height = 440;

            taskSearchWindow.Content = taskview;

            bool? res = taskSearchWindow.ShowDialog();

            if (res.HasValue)
            {
                System.Diagnostics.Debug.WriteLine("res value" + res);
                if ((bool)res)
                {
                    System.Diagnostics.Debug.WriteLine("task showdialog true");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("task showdialog false");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("task showdialog null");
            }
        }
    }
}
