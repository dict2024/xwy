﻿using Android.Content;
using Android.Graphics;
using Android.InputMethodServices;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace Exercises
{
    public class TitleButton : View
    {
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
                canvas.DrawRoundRect(0, -40, 400, 60, 60, 60, blackPaint);
                canvas.DrawRoundRect(400, -40, 800, 60, 60, 60, grayPaint);
                canvas.DrawText("查字典", 150, 40, whiteTextPaint);
                canvas.DrawText("做练习", 550, 40, whiteTextPaint);
            } else
            {
                canvas.DrawRoundRect(0, -40, 400, 60, 60, 60, grayPaint);
                canvas.DrawRoundRect(400, -40, 800, 60, 60, 60, blackPaint);
                canvas.DrawText("查字典", 150, 40, whiteTextPaint);
                canvas.DrawText("做练习", 550, 40, whiteTextPaint);
            }

            base.OnDraw(canvas);
            //Color color = Color.Argb(255, colors[i, 1], colors[i, 2], colors[i, 3]);

        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e.GetX() < 400)
            {
                mode = 0;
            }
            else
            {
                mode = 1;
            }
            this.Invalidate();
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
