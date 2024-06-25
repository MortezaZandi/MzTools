using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideToolbars
{
    internal class ControlAnimation
    {
        private Control control;
        private AnimationDirectionType animationDirection;
        private bool isCollapseAnimation;
        private System.Windows.Forms.Timer animationTimer;
        private DateTime animationStartTime;
        private int animationDuration = 200;
        private int startValue;
        private int targetValue;

        public ControlAnimation(Control control, int targetValue, AnimationDirectionType animationDirection = AnimationDirectionType.None)
        {
            this.control = control;
            this.targetValue = targetValue;

            if (animationDirection == AnimationDirectionType.None)
            {
                switch (control.Dock)
                {
                    case DockStyle.Top:
                        this.animationDirection = AnimationDirectionType.TopToBottom;
                        break;

                    case DockStyle.Bottom:
                        this.animationDirection = AnimationDirectionType.BottomToTop;
                        break;

                    case DockStyle.Left:
                        this.animationDirection = AnimationDirectionType.LeftToRight;
                        break;

                    case DockStyle.Right:
                        this.animationDirection = AnimationDirectionType.RightToLeft;
                        break;

                    case DockStyle.Fill:
                    case DockStyle.None:
                    default:
                        throw new ArgumentException("Animation type should set when control dock mode is not set");
                }
            }
            else
            {
                this.animationDirection = animationDirection;
            }

            switch (this.animationDirection)
            {
                case AnimationDirectionType.LeftToRight:
                    isCollapseAnimation = (targetValue < control.Width);
                    this.startValue = control.Width;
                    break;

                case AnimationDirectionType.RightToLeft:
                    isCollapseAnimation = (targetValue < control.Width);
                    this.startValue = control.Width;
                    break;

                case AnimationDirectionType.TopToBottom:
                    isCollapseAnimation = (targetValue < control.Height);
                    this.startValue = control.Height;
                    break;

                case AnimationDirectionType.BottomToTop:
                    isCollapseAnimation = (targetValue < control.Height);
                    this.startValue = control.Height;
                    break;

                case AnimationDirectionType.None:
                default:
                    break;
            }


            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 1;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (animationStartTime == DateTime.MinValue)
            {
                animationStartTime = DateTime.Now;
            }

            var deltaTime = (DateTime.Now - animationStartTime).TotalMilliseconds / animationDuration;

            if (deltaTime <= 1)
            {
                switch (animationDirection)
                {
                    case AnimationDirectionType.LeftToRight:
                        control.Width = NextValue(deltaTime);
                        break;
                    case AnimationDirectionType.RightToLeft:
                        var nextValue = NextValue(deltaTime);
                        var change = control.Width - nextValue;
                        control.Width = nextValue;
                        control.Left += change;
                        break;

                    case AnimationDirectionType.TopToBottom:
                        control.Height = NextValue(deltaTime);
                        break;
                    case AnimationDirectionType.BottomToTop:
                        var nextValue2 = NextValue(deltaTime);
                        var change2 = control.Height - nextValue2;
                        control.Height = nextValue2;
                        control.Top += change2;
                        break;
                    case AnimationDirectionType.None:
                    default:
                        break;
                }
            }
            else
            {
                switch (animationDirection)
                {
                    case AnimationDirectionType.LeftToRight:
                    case AnimationDirectionType.RightToLeft:
                        control.Width = targetValue;
                        break;

                    case AnimationDirectionType.TopToBottom:
                    case AnimationDirectionType.BottomToTop:
                        control.Height = targetValue;
                        break;

                    case AnimationDirectionType.None:
                    default:
                        break;
                }

                animationTimer.Stop();

                animationTimer = null;//animation is not reusable
            }
        }

        public void Start()
        {
            animationTimer.Start();
        }

        private int NextValue(double deltaTime)
        {
            var len = (targetValue - startValue);

            return (int)(startValue + (len * deltaTime));
        }
    }


    public enum AnimationDirectionType
    {
        None,
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop,
    }


}

