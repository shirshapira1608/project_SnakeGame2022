using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Image = Windows.UI.Xaml.Controls.Image;

namespace FinalProject.Classes
{
    class Snake
    {
        private Canvas arena;
        private List<Node> list;
        public User user { get; set; }
        public DirectionType Direction { get; set; }
        private DispatcherTimer moveTimer;
        private Food food1;
        private Food food2;
        private bool wallKill;
        private bool isPaused;
        private Button startOver;
        private TextBlock message;
        public Snake(Canvas arena, Food food1, Food food2, int mode, User user)
        {
            this.user = user;
            this.isPaused = false;
            if (mode == 1)
                wallKill = false;
            else
                wallKill = true;
            this.food1 = food1;
            this.food2 = food2;
            this.list = new List<Node>();
            this.Direction = DirectionType.up;
            this.arena = arena;
            CreateSnake();
            this.message = new TextBlock();
            this.startOver = new Button();
            this.moveTimer = new DispatcherTimer();
            this.moveTimer.Start();
            this.moveTimer.Interval = TimeSpan.FromMilliseconds(150);
            this.moveTimer.Tick += MoveTimer_Tick;
        }
        private void RestartVerifiction(int score)
        {
            bool isMaxScore = score > user.MaxScore;
            this.startOver.Content = "Start Over";
            this.startOver.FontSize = 22;
            this.startOver.Height = 50;
            this.startOver.Width = 150;
            double x = this.arena.ActualWidth / 2 - 75;
            double y = this.arena.ActualHeight / 2 - 25;
            Canvas.SetLeft(startOver, x);
            Canvas.SetTop(startOver, y);
            this.startOver.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.startOver.VerticalAlignment = VerticalAlignment.Stretch;
            this.startOver.CornerRadius = new CornerRadius(20);
            this.startOver.BorderBrush = new SolidColorBrush(Colors.White);
            this.startOver.BorderThickness = new Thickness(2);
            this.startOver.Margin = new Thickness(5);
            this.startOver.Click += StartOver_Click;
            this.arena.Children.Add(this.startOver);

            if (score == -1)
                score = 0;
            if (!isMaxScore)
            {
                if (score >= 10 && score < 25)
                {
                    this.message.Text = $"You got {score} points !!! good job! \n do you want to start over?";
                    Canvas.SetLeft(this.message, this.arena.ActualWidth / 2.8);
                }
                else if (score >= 25)
                {
                    this.message.Text = $"You got {score} points !!! DAMNN! \n I think you should Try to bypass your record";
                    Canvas.SetLeft(this.message, this.arena.ActualWidth / 3.1);
                }
                else
                {
                    this.message.Text = $"You got {score} points !!! disappointing... \n I think you can score more";
                    Canvas.SetLeft(this.message, this.arena.ActualWidth / 2.9);
                }
            }
            else
            {
                this.message.Text = $"You got {score} points !!! good job! \n new best score! AMAZING!";
                Canvas.SetLeft(this.message, this.arena.ActualWidth / 2.8);
            }
            this.message.FontSize = 30;
            this.message.TextAlignment = TextAlignment.Center;
            this.message.FontWeight = Windows.UI.Text.FontWeights.Bold;
            this.message.FontFamily = new Windows.UI.Xaml.Media.FontFamily("Bradley Hand ITC");
            arena.Children.Add(this.message);
            Canvas.SetTop(this.message, this.arena.ActualHeight / 3);
        }
        private void StartOver_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                VisibilityOfNode(true, i);
            }
            this.food1.Image.Visibility = Visibility.Visible;
            this.food2.Image.Visibility = Visibility.Visible;
            this.arena.Children.Remove(this.startOver);
            this.arena.Children.Remove(this.message);
            this.isPaused = false;
        }
        private void CreateSnake()
        {
            Node node1 = new Node(arena, arena.ActualWidth/2, arena.ActualHeight/2, false, 50, this.Direction, this.user);
            this.list.Add(node1);
            Node node2 = new Node(arena, arena.ActualWidth/2, arena.ActualHeight/2 - node1.GetSize() * this.list.Count, false, 50, this.Direction, this.user);
            this.list.Add(node2);
            Node node3 = new Node(arena, arena.ActualWidth/2, arena.ActualHeight/2 - node1.GetSize() * this.list.Count, false, 50, this.Direction, this.user);
            this.list.Add(node3);
            Node node4 = new Node(arena, arena.ActualWidth/2, arena.ActualHeight/2 - node1.GetSize() * this.list.Count, false, 50, this.Direction, this.user);
            this.list.Add(node4);
            Node head = new Node(arena, arena.ActualWidth / 2, arena.ActualHeight / 2 - node1.GetSize() * this.list.Count, true, 50, this.Direction, this.user);
            this.list.Add(head);
        }
        private void MoveTimer_Tick(object sender, object e)
        {
            if (!isPaused)
            {
                TextBlock score = (TextBlock)this.arena.FindName("score");
                IsMoreThenTwenty();
                AddNode();
                RemoveNode();
                UpdateScore(score);
                CollisionWithNode();
                if (int.Parse(score.Text) < 0)
                    RestartGame();
            }
        }
        public void UpdateScore(TextBlock score)
        {
            if (Collision(this.food1))
            {
                AddNode();
                this.food1.Replace();
                this.food2.Replace();
                score.Text = (int.Parse(score.Text) + this.food1.Point).ToString();
            }
            if (Collision(this.food2))
            {
                AddNode();
                this.food1.Replace();
                this.food2.Replace();
                score.Text = (int.Parse(score.Text) + this.food2.Point).ToString();
            }
        }
        public void AddNode()
        {
            Node node = null;
            this.list[list.Count - 1].ChangeHead(false);
            switch (this.Direction)
            {
                case DirectionType.up:
                    if ((int)this.list[list.Count - 1].PlaceY > 0)
                    {
                        node = new Node(arena, (int)this.list[list.Count - 1].PlaceX, (int)this.list[list.Count - 1].PlaceY - 50, true, 50, this.Direction, this.user);
                        this.list.Add(node);
                    }
                    else
                    {
                        if (wallKill)
                            RestartGame();
                        else
                        {
                            node = new Node(arena, (int)this.list[list.Count - 1].PlaceX, arena.ActualHeight - 50, true, 50, this.Direction, this.user);
                            this.list.Add(node);
                        }
                    }
                    break;
                case DirectionType.down:
                    if ((int)this.list[list.Count - 1].PlaceY < arena.ActualHeight)
                    {
                        node = new Node(arena, (int)this.list[list.Count - 1].PlaceX, (int)this.list[list.Count - 1].PlaceY + 50, true, 50, this.Direction, this.user);
                        this.list.Add(node);
                    }
                    else
                    {
                        if (wallKill)
                            RestartGame();
                        else
                        {
                            node = new Node(arena, (int)this.list[list.Count - 1].PlaceX, 0, true, 50, this.Direction, this.user);
                            this.list.Add(node);
                        }
                    }
                    break;
                case DirectionType.right:
                    if ((int)this.list[list.Count - 1].PlaceX < arena.ActualWidth)
                    {
                        node = new Node(arena, (int)this.list[list.Count - 1].PlaceX + 50, (int)this.list[list.Count - 1].PlaceY, true, 50, this.Direction, this.user);
                        this.list.Add(node);
                    }
                    else
                    {
                        if (wallKill)
                            RestartGame();
                        else
                        {
                            node = new Node(arena, 0, (int)this.list[list.Count - 1].PlaceY, true, 50, this.Direction, this.user);
                            this.list.Add(node);
                        }
                    }
                    break;
                case DirectionType.left:
                    if ((int)this.list[list.Count - 1].PlaceX > 0)
                    {
                        node = new Node(arena, (int)this.list[list.Count - 1].PlaceX - 50, (int)this.list[list.Count - 1].PlaceY, true, 50, this.Direction, this.user);
                        this.list.Add(node);
                    }
                    else
                    {
                        if (wallKill)
                            RestartGame();
                        else
                        {
                            node = new Node(arena, arena.ActualWidth - 50, (int)this.list[list.Count - 1].PlaceY, true, 50, this.Direction, this.user);
                            this.list.Add(node);
                        }
                    }
                    break;
            }
        }
        public void RemoveNode()
        {
            this.arena.Children.Remove(this.list[0].Img);
            this.list.RemoveAt(0);
        }
        public void VisibilityOfNode(bool visible, int i)
        {
            if (visible)
                this.list[i].Img.Visibility = Visibility.Visible;
            else
                this.list[i].Img.Visibility = Visibility.Collapsed;
        }
        public enum DirectionType
        {
            left, up, right, down
        }
        public bool Collision(Food food)
        {
            double ax1, ax2, ay1, ay2;
            Image a = this.list[this.list.Count - 1].Img;
            double bx1, bx2, by1, by2;
            Image b = food.Image;

            ax1 = Canvas.GetLeft(a);
            ax2 = a.Width + ax1;
            ay1 = Canvas.GetTop(a);
            ay2 = a.Height + ay1;

            bx1 = Canvas.GetLeft(b);
            bx2 = b.Width + bx1;
            by1 = Canvas.GetTop(b);
            by2 = b.Height + by1;

            bool side_ab = ax1 < bx2 && ax2 > bx1;
            bool top_ab = ay1 < by2 && ay2 > by1;

            if (side_ab)
            {
                if (top_ab)
                {
                    return true;
                }
            }
            return false;
        }
        public void CollisionWithNode()
        {
            for (int i = 0; i < this.list.Count-1; i++)
            {
                double ax1, ax2, ay1, ay2;
                Image a = this.list[this.list.Count - 1].Img;
                double bx1, bx2, by1, by2;
                Image b = list[i].Img;

                ax1 = Canvas.GetLeft(a);
                ax2 = a.Width + ax1;
                ay1 = Canvas.GetTop(a);
                ay2 = a.Height + ay1;

                bx1 = Canvas.GetLeft(b);
                bx2 = b.Width + bx1;
                by1 = Canvas.GetTop(b);
                by2 = b.Height + by1;

                bool side_ab = ax1 < bx2 && ax2 > bx1;
                bool top_ab = ay1 < by2 && ay2 > by1;

                if (side_ab)
                {
                    if (top_ab)
                    {
                        RestartGame();
                    }
                }
            }
        }
        public void IsMoreThenTwenty()
        {
            TextBlock score = (TextBlock)this.arena.FindName("score");
            if (int.Parse(score.Text) >= 20)
            {
                this.moveTimer.Interval = TimeSpan.FromMilliseconds(75);
            }
        }
        public void RestartGame()
        {
            for (int i = 0; i < this.list.Count;)
            {
                RemoveNode();
            }
            CreateSnake();
            for (int i = 0; i < this.list.Count; i++)
            {
                VisibilityOfNode(false, i);
            }

            this.food1.Image.Visibility = Visibility.Collapsed;
            this.food2.Image.Visibility = Visibility.Collapsed;
            this.moveTimer.Interval = TimeSpan.FromMilliseconds(150);
            TextBlock score = (TextBlock)this.arena.FindName("score");
            this.Direction = DirectionType.up;
            int currentScore = int.Parse(score.Text);
            this.user = DataBaseMethods.UpdateMaxScore(user ,currentScore);
            RestartVerifiction(currentScore);
            score.Text = "0";
            this.isPaused = true;
        }
    }
}
