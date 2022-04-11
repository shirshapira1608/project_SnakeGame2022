using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FinalProject.Classes
{
    class Manager
    {
        private Canvas arena;
        public Snake Snake { get; set; }
        public GoodFood GoodFood { get; set; }
        public BadFood BadFood { get; set; }
        public User UseR { get; set; }
        public Manager(Canvas arena, int mode, User user)
        {
            this.UseR = user;
            this.arena = arena;
            string goodUri = "";
            string badUri = "";
            switch (user.CurrentFood)
            {
                case 0:
                    goodUri = "ms-appx:///Assets/pic/food/apple.png";
                    badUri = "ms-appx:///Assets/pic/food/badApple.png";
                    break;
                case 1:
                    goodUri = "ms-appx:///Assets/pic/food/banana.png";
                    badUri = "ms-appx:///Assets/pic/food/badBanana.png";
                    break;
                case 2:
                    goodUri = "ms-appx:///Assets/pic/food/cherry.png";
                    badUri = "ms-appx:///Assets/pic/food/badCherry.png";
                    break;
                case 3:
                    goodUri = "ms-appx:///Assets/pic/food/strawberry.png";
                    badUri = "ms-appx:///Assets/pic/food/badStrawberry.png";
                    break;
            }
            this.GoodFood = new GoodFood(arena, goodUri);
            this.BadFood = new BadFood(arena, badUri);
            this.Snake = new Snake(this.arena, GoodFood, BadFood, mode, user);
        }
    }
}
