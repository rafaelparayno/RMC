using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Animation
{
   public static class HideImageByOpaque
    {
      

        public static void showFormOpacity(Form ctr1,int ms)
        {
            ctr1.Visible = false;
            ctr1.Opacity = 0;
            for(int i = 0; i <=ms; i++)
            {
                ctr1.Visible = true;
                ctr1.Opacity += .10;
            }
        }
    }
}
