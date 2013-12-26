﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;


/**
 * 
 *                   var point = se.TransformToVisual(MainContentWrapper).TransformPoint(new Point(0, 0));
 */
namespace ManipulationModeDemo
{
    public partial class MainWindow : Window
    {
        #region Instance Definition
        ManipulationModes currentMode = ManipulationModes.All;
        #endregion 

        #region public MainWindow
        public MainWindow()
        {
            InitializeComponent();

            currentMode = ManipulationModes.Scale | ManipulationModes.Translate;

            
            
        }
        #endregion 

        //#region void OnRadioChecked
        //void OnRadioChecked(object sender, RoutedEventArgs args)
        //{
        //    currentMode = (ManipulationModes)(sender as RadioButton).Content;
        //}
        //#endregion 

        #region protected override void OnManipulationStarting
        protected override void OnManipulationStarting(ManipulationStartingEventArgs args)
        {
            args.ManipulationContainer = this;
            args.Mode = currentMode;

            // Adjust Z-order
            //FrameworkElement element = args.Source as FrameworkElement;
            //Panel pnl = element.Parent as Panel;

            //for (int i = 0; i < pnl.Children.Count; i++)
            //    Panel.SetZIndex(pnl.Children[i],
            //        pnl.Children[i] == element ? pnl.Children.Count : i);

            args.Handled = true;
            base.OnManipulationStarting(args);
        }
        #endregion 

        #region protected override void OnManipulationDelta
        protected override void OnManipulationDelta(ManipulationDeltaEventArgs args)
        {
            UIElement element = args.Source as UIElement;
            MatrixTransform xform = element.RenderTransform as MatrixTransform;
            Matrix matrix = xform.Matrix;
            ManipulationDelta delta = args.DeltaManipulation;
            Point center = args.ManipulationOrigin;

            Matrix to = matrix;
            to.Translate(-center.X, -center.Y);
            to.Scale(delta.Scale.X, delta.Scale.Y);
            to.Rotate(delta.Rotation);
            to.Translate(center.X, center.Y);
            to.Translate(delta.Translation.X, delta.Translation.Y);

            MatrixAnimation b = new MatrixAnimation()
            {
                From = matrix,
                To = to,
                Duration = TimeSpan.FromMilliseconds(0),
                FillBehavior = FillBehavior.HoldEnd
            };
            (element.RenderTransform as MatrixTransform).BeginAnimation(MatrixTransform.MatrixProperty, b);



            tbTranslate.Text = string.Format("Translation: {0}, {1}", delta.Translation.X, delta.Translation.Y);
            tbTranslate.Text += string.Format("\r\nTotal Translation: {0}, {1}", args.CumulativeManipulation.Translation.X, args.CumulativeManipulation.Translation.Y);
          
            args.Handled = true;
            base.OnManipulationDelta(args);
        }
        #endregion 

        #region protected override void OnManipulationCompleted
        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
          
          tbCompleted.Text = string.Format("{0}", e.FinalVelocities.LinearVelocity);
          tbCompleted.Text += string.Format("\r\n{0}", e.TotalManipulation.Translation);
          //UIElement el = e.Source as UIElement;
          //el.Effect = new BlurEffect() { Radius= 10.0};

          //MatrixTransform xform = el.RenderTransform as MatrixTransform;
          //Matrix matrix = xform.Matrix;
          //Matrix from = matrix;
          //Matrix to = matrix;
          //to.Translate(e.TotalManipulation.Translation.X * Math.Abs(e.FinalVelocities.LinearVelocity.X), e.TotalManipulation.Translation.Y * Math.Abs(e.FinalVelocities.LinearVelocity.Y));

          //if (Math.Abs(e.FinalVelocities.LinearVelocity.X) > 0.5 || Math.Abs(e.FinalVelocities.LinearVelocity.Y) > 0.5)
          //{
          //  MatrixAnimation b = new MatrixAnimation()
          //  {
          //    From = from,
          //    To = to,
          //    Duration = TimeSpan.FromMilliseconds(500),
          //    FillBehavior = FillBehavior.HoldEnd
          //  };
          //  (el.RenderTransform as MatrixTransform).BeginAnimation(MatrixTransform.MatrixProperty, b);
          //}

          
          base.OnManipulationCompleted(e);
        }
        #endregion 

        #region private void Grid_Loaded
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {            
            Play_StoryBoard("fadeout");
            Play_StoryBoard("hide");
            Play_StoryBoard("moveinit");
        }
        #endregion 

        #region private void player1_MouseUp
        private void player1_MouseUp(object sender, MouseButtonEventArgs e)
        {
          if (sender.GetType().ToString() == "System.Windows.Controls.MediaElement")
          {
            MediaElement me = sender as MediaElement;
            MessageBox.Show("11");
          }
        }
        #endregion 

        #region private void player1_StylusSystemGesture
        private void player1_StylusSystemGesture(object sender, StylusSystemGestureEventArgs e)
        {
          if (e.SystemGesture == SystemGesture.Tap)
          {
          }
        }
        #endregion 

        private void onTouchImage1(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0000_지천년.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);  
            TouchContentMethod();
        }   

        private void onTouchImage2(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0001_염색.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage3(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0002_백운예술제.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage4(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0003_기차축제.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage5(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0004_13하우현.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage6(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0005_12청계사.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage7(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0006_11백운계곡.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage8(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0007_10맑은숲공원.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage9(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0008_9갈미.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage10(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0009_8모락산.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage11(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0010_7백운산.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage12(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0011_6청계산.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage13(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0012_5조류생태.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage14(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0013_4철도박물관.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();

        }
        private void onTouchImage15(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0014_3자연학습공원.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage16(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0015_2왕송호수.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();
        }
        private void onTouchImage17(object sender, TouchEventArgs e)
        {
            var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0016_백운호수.jpg", UriKind.Relative);
            popup_image.Source = new BitmapImage(uriSource);
            TouchContentMethod();            
        }

        // 바깥 터치시 종료 애니메이션 [12/24/2013 Mark]
        private void onTouchFadeRect(object sender, TouchEventArgs e)
        {
            Play_StoryBoard("hide");
            Play_StoryBoard("fadeout");
        }

        // 터치하면 이미지 애니메이션으로 띄움. [12/24/2013 Mark]
        private void TouchContentMethod()
        {
            popup_image.Visibility = Visibility.Visible;
            rct_fadeout.Visibility = Visibility.Visible;

            Play_StoryBoard("fadein");
            Play_StoryBoard("show");
        }

        private void Play_StoryBoard(string strKey)
        {
            var stybd = this.Resources[strKey] as Storyboard;
            if (stybd != null)
                stybd.Begin();
        }


        //delegate void DelegateMethod();
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //System.Threading.Tasks.Task.Factory.StartNew(Run); 
            //DelegateMethod d;
            if (e.Key == Key.F2)
            {
                var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0000_지천년.jpg", UriKind.Relative);
                popup_image.Source = new BitmapImage(uriSource);  
                TouchContentMethod();
            }
            if (e.Key == Key.F3)
            {
                var uriSource = new Uri(@"/MapProject;component/Images/popup2/map_0001_염색.jpg", UriKind.Relative);
                popup_image.Source = new BitmapImage(uriSource);
                TouchContentMethod();
                
            }

            if (e.Key == Key.F5)
            {
                Play_StoryBoard("show");
            }

            if (e.Key == Key.F6)
            {
                Play_StoryBoard("hide");
            }
            

            if (e.Key == Key.Escape)
            {
                //rct_fadeout.Visibility = Visibility.Hidden;
                //popup_image.Visibility = Visibility.Hidden;

                Play_StoryBoard("hide");
                Play_StoryBoard("fadeout");
            }

        }

        // 맵 좌표 초기화가 끝나면
        private void StoryInitCompleted(object sender, EventArgs e)
        {
            rct_fadeout.Visibility = Visibility.Hidden;
            popup_image.Visibility = Visibility.Hidden;
        }

        private void StoryHideCompleted(object sender, EventArgs e)
        {
            rct_fadeout.Visibility = Visibility.Hidden;
            popup_image.Visibility = Visibility.Hidden;
        }






    }

    

    #region public class MatrixAnimation
    public class MatrixAnimation : MatrixAnimationBase
    {
      public Matrix? From
      {
        set { SetValue(FromProperty, value); }
        get { return (Matrix)GetValue(FromProperty); }
      }

      public static DependencyProperty FromProperty =
          DependencyProperty.Register("From", typeof(Matrix?), typeof(MatrixAnimation),
              new PropertyMetadata(null));

      public Matrix? To
      {
        set { SetValue(ToProperty, value); }
        get { return (Matrix)GetValue(ToProperty); }
      }

      public static DependencyProperty ToProperty =
          DependencyProperty.Register("To", typeof(Matrix?), typeof(MatrixAnimation),
              new PropertyMetadata(null));

      public IEasingFunction EasingFunction
      {
        get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
        set { SetValue(EasingFunctionProperty, value); }
      }

      public static readonly DependencyProperty EasingFunctionProperty =
          DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(MatrixAnimation),
              new UIPropertyMetadata(null));

      public MatrixAnimation()
      {
      }

      public MatrixAnimation(Matrix toValue, Duration duration)
      {
        To = toValue;
        Duration = duration;
      }

      public MatrixAnimation(Matrix toValue, Duration duration, FillBehavior fillBehavior)
      {
        To = toValue;
        Duration = duration;
        FillBehavior = fillBehavior;
      }

      public MatrixAnimation(Matrix fromValue, Matrix toValue, Duration duration)
      {
        From = fromValue;
        To = toValue;
        Duration = duration;
      }

      public MatrixAnimation(Matrix fromValue, Matrix toValue, Duration duration, FillBehavior fillBehavior)
      {
        From = fromValue;
        To = toValue;
        Duration = duration;
        FillBehavior = fillBehavior;
      }

      protected override Freezable CreateInstanceCore()
      {
        return new MatrixAnimation();
      }

      protected override Matrix GetCurrentValueCore(Matrix defaultOriginValue, Matrix defaultDestinationValue, AnimationClock animationClock)
      {
        if (animationClock.CurrentProgress == null)
        {
          return Matrix.Identity;
        }

        var normalizedTime = animationClock.CurrentProgress.Value;
        if (EasingFunction != null)
        {
          normalizedTime = EasingFunction.Ease(normalizedTime);
        }

        var from = From ?? defaultOriginValue;
        var to = To ?? defaultDestinationValue;

        var newMatrix = new Matrix(
                ((to.M11 - from.M11) * normalizedTime) + from.M11,
                ((to.M12 - from.M12) * normalizedTime) + from.M12,
                ((to.M21 - from.M21) * normalizedTime) + from.M21,
                ((to.M22 - from.M22) * normalizedTime) + from.M22,
                ((to.OffsetX - from.OffsetX) * normalizedTime) + from.OffsetX,
                ((to.OffsetY - from.OffsetY) * normalizedTime) + from.OffsetY);

        return newMatrix;
      }
    }
    #endregion 

    #region public class LinearMatrixAnimation 
    public class LinearMatrixAnimation : AnimationTimeline
    {

      public Matrix? From
      {
        set { SetValue(FromProperty, value); }
        get { return (Matrix)GetValue(FromProperty); }
      }
      public static DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Matrix?), typeof(LinearMatrixAnimation), new PropertyMetadata(null));

      public Matrix? To
      {
        set { SetValue(ToProperty, value); }
        get { return (Matrix)GetValue(ToProperty); }
      }
      public static DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Matrix?), typeof(LinearMatrixAnimation), new PropertyMetadata(null));

      public LinearMatrixAnimation()
      {
      }

      public LinearMatrixAnimation(Matrix from, Matrix to, Duration duration)
      {
        Duration = duration;
        From = from;
        To = to;
      }

      public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
      {
        if (animationClock.CurrentProgress == null)
        {
          return null;
        }

        double progress = animationClock.CurrentProgress.Value;
        Matrix from = From ?? (Matrix)defaultOriginValue;

        if (To.HasValue)
        {
          Matrix to = To.Value;
          Matrix newMatrix = new Matrix(((to.M11 - from.M11) * progress) + from.M11, 0, 0, ((to.M22 - from.M22) * progress) + from.M22,
                                        ((to.OffsetX - from.OffsetX) * progress) + from.OffsetX, ((to.OffsetY - from.OffsetY) * progress) + from.OffsetY);
          return newMatrix;
        }

        return Matrix.Identity;
      }

      protected override System.Windows.Freezable CreateInstanceCore()
      {
        return new LinearMatrixAnimation();
      }

      public override System.Type TargetPropertyType
      {
        get { return typeof(Matrix); }
      }
    }
    #endregion 
}