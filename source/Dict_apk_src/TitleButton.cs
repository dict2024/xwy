using Android.Content;
using Android.Graphics;
using Android.InputMethodServices;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace Dict
{
    public class TitleButton : View
    {
        private int titleWidth = 1080;
        public int mode = 0;
        public TitleButton(Context? context) : base(context)
        {

        }

        protected override void OnDraw(Canvas? canvas)
        {
            Paint blackPaint = new Paint
            {
                Color = Color.Black,
            };
            Paint grayPaint = new Paint
            {
                Color = Color.Gray,
            };
            Paint whiteTextPaint = new Paint
            {
                Color = Color.White,
                TextSize = 40
            };
            if (mode == 0)
            {
                canvas.DrawRoundRect(10, -50, titleWidth / 2 - 5, 60, 60, 60, blackPaint);
                canvas.DrawRoundRect(5 + titleWidth / 2, -50, titleWidth - 10, 60, 60, 60, grayPaint);
                canvas.DrawText("查字典", titleWidth / 4 - 40, 40, whiteTextPaint);
                canvas.DrawText("做练习", titleWidth - titleWidth / 4 - 40, 40, whiteTextPaint);
            }
            else
            {
                canvas.DrawRoundRect(10, -50, titleWidth / 2 - 5, 60, 60, 60, grayPaint);
                canvas.DrawRoundRect(5 + titleWidth / 2, -50, titleWidth - 10, 60, 60, 60, blackPaint);
                canvas.DrawText("查字典", titleWidth / 4 - 40, 40, whiteTextPaint);
                canvas.DrawText("做练习", titleWidth - titleWidth / 4 - 40, 40, whiteTextPaint);
            }

            base.OnDraw(canvas);
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e.GetX() < titleWidth / 2)
            {
                mode = 0;
                this.Invalidate();
            }
            else if (e.GetX() > titleWidth / 2 + 20)
            {
                mode = 1;
                this.Invalidate();
            }
            else
            {
                
            }
            
            return base.OnTouchEvent(e);
        }

        public TitleButton(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
        }

        public TitleButton(Context? context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public TitleButton(Context? context, IAttributeSet? attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected TitleButton(nint javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}
