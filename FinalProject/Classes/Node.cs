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
using static FinalProject.Classes.Snake;

namespace FinalProject.Classes
{
    class Node
    {
        public double PlaceX{get; }
        public User user;
        public double PlaceY{get; }
        private double size;
        public Image Img {get; }
        private Canvas arena;
        public Node(Canvas arena, double placeX, double placeY, bool head, double size, DirectionType direction, User user)
        {
            this.user = user;
            this.arena = arena;
            this.Img = new Image();
            this.size = size;
            this.Img.Width = this.size;
            this.Img.Height = this.size;
            switch (user.CurrentCharacter)
            {
                case 0:
                    if (head)
                    {
                        switch (direction)
                        {
                            case DirectionType.up:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/whiteHead/whiteHeadUp.png"));
                                break;
                            case DirectionType.left:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/whiteHead/whiteHeadLeft.png"));
                                break;
                            case DirectionType.down:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/whiteHead/whiteHeadDown.png"));
                                break;
                            case DirectionType.right:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/whiteHead/whiteHeadRight.png"));
                                break;
                        }
                    }
                    else
                    {
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/whiteHead/whiteNode.png"));
                    }
                    break;
                case 1:
                    if (head)
                    {
                        switch (direction)
                        {
                            case DirectionType.up:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/pinkHead/pinkHeadUp.png"));
                                break;
                            case DirectionType.left:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/pinkHead/pinkHeadLeft.png"));
                                break;
                            case DirectionType.down:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/pinkHead/pinkHeadDown.png"));
                                break;
                            case DirectionType.right:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/pinkHead/pinkHeadRight.png"));
                                break;
                        }
                    }
                    else
                    {
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/pinkHead/pinkNode.png"));
                    }
                    break;
                case 2:
                    if (head)
                    {
                        switch (direction)
                        {
                            case DirectionType.up:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/yellowHead/yellowHeadUp.png"));
                                break;
                            case DirectionType.left:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/yellowHead/yellowHeadLeft.png"));
                                break;
                            case DirectionType.down:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/yellowHead/yellowHeadDown.png"));
                                break;
                            case DirectionType.right:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/yellowHead/yellowHeadRight.png"));
                                break;
                        }
                    }
                    else
                    {
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/yellowHead/yellowNode.png"));
                    }
                    break;
                case 3:
                    if (head)
                    {
                        switch (direction)
                        {
                            case DirectionType.up:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/blueHead/blueHeadUp.png"));
                                break;
                            case DirectionType.left:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/blueHead/blueHeadLeft.png"));
                                break;
                            case DirectionType.down:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/blueHead/blueHeadDown.png"));
                                break;
                            case DirectionType.right:
                                this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/blueHead/blueHeadRight.png"));
                                break;
                        }
                    }
                    else
                    {
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/blueHead/blueNode.png"));
                    }
                    break;
            }

            this.PlaceX = placeX;
            this.PlaceY = placeY;
            this.arena.Children.Add(this.Img);
            Canvas.SetLeft(this.Img, this.PlaceX);
            Canvas.SetTop(this.Img, this.PlaceY);
        }
        public double GetSize() => this.size;
        public void ChangeHead(bool head)
        {
            switch (this.user.CurrentCharacter)
            {
                case 0:
                    if (head)
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/whiteHead/whiteHeadUp.png"));
                    else
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/whiteHead/whiteNode.png"));
                    
                    break;
                case 1:
                    if (head)
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/pinkHead/pinkHeadUp.png"));
                    else
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/pinkHead/pinkNode.png"));
                    
                    break;
                case 2:
                    if (head)
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/yellowHead/yellowHeadUp.png"));
                    else
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/yellowHead/yellowNode.png"));
                    
                    break;
                case 3:
                    if (head)
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/blueHead/blueHeadUp.png"));
                    else
                        this.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic/head/blueHead/blueNode.png"));
                    
                    break;

            }
        }

    }
}
