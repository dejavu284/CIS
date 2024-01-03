using CinemaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfAppCIS.ViewModel;

namespace WpfAppCIS.Model
{
    public class WindowPartView
    {
        public WindowPartView(ContentControl contentControl)
        {
            _contentControl = contentControl;
        }
        private ContentControl _contentControl;
       
        public void LoadView(UserControl userControl)
        {
            _contentControl.Content = userControl;
        }
    }
}
