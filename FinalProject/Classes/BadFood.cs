using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FinalProject.Classes
{
    class BadFood:Food
    {
        public BadFood(Canvas arena, string uri) : base(arena, -1, uri, 50)
        {
        }
    }
}
