using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProject.Classes
{
    class GoodFood:Food
    {
        public GoodFood(Canvas arena, string uri) : base(arena, 1, uri, 50)
        {
        }
    }
}
