﻿using System.Collections;
using UnityEngine;

/// <summary>
/// This script, when attached to a panel turns it into a paged scroll view.
/// You can then attach UIDragPageView to colliders within to make it draggable.
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Page View")]
public class UIPageView : UIScrollView
{
    /// <summary>
    /// The item width
    /// </summary>
    public float itemWidth = 300.0f;

    /// <summary>
    /// The item height
    /// </summary>
    public float itemHeight = 200.0f;

    /// <summary>
    /// The current page index
    /// </summary>
    private int currentPageIndex = 1;

    /// <summary>
    /// Gets the current page.
    /// </summary>
    /// <value>The current page.</value>
    public int currentPage
    {
        get { return currentPageIndex; }
    }

    /// <summary>
    /// The touch begin position
    /// </summary>
    private Vector2 touchBeginPosition;

    /// <summary>
    /// The touch end position
    /// </summary>
    private Vector2 touchEndPosition;

    /// <summary>
    /// The allow page turn
    /// </summary>
    private bool allowPageTurn = true;

    /// <summary>
    /// Gets the page turn measure.
    /// </summary>
    /// <value>The page turn measure.</value>
    public float pageTurnMeasure
    {
        get
        {
            if (movement == Movement.Horizontal)
                return itemWidth / 2;
            else if (movement == Movement.Vertical)
                return itemHeight / 2;

            return float.MaxValue;
        }
    }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    /// <value>The total pages.</value>
    public int totalPages
    {
        get
        {
            Bounds b = bounds;

            if (movement == Movement.Horizontal)
                return Mathf.CeilToInt(b.size.x / itemWidth);
            else if (movement == Movement.Vertical)
                return Mathf.CeilToInt(b.size.y / itemHeight);

            return 0;
        }
    }

    /// <summary>
    /// Create a plane on which we will be performing the dragging.
    /// </summary>
    /// <param name="pressed">The pressed.</param>
    public override void Press(bool pressed)
    {
        if (pressed)
        {
            // touch begin
            touchBeginPosition = UICamera.currentTouch.pos;
        }
        else
        {
            if (!allowPageTurn)
                return;

            // touch end
            touchEndPosition = UICamera.currentTouch.pos;

            // touch point offset
            Vector2 touchOffset = touchEndPosition - touchBeginPosition;

            if (movement == Movement.Horizontal)
            {
                if (touchOffset.x > 0 && touchOffset.x >= pageTurnMeasure)
                {
                    // turn to previous page
                    GotoPage(currentPageIndex - 1);
                }
                else if (touchOffset.x < 0 && touchOffset.x <= -pageTurnMeasure)
                {
                    // turn to next page
                    GotoPage(currentPageIndex + 1);
                }
            }
            else if (movement == Movement.Vertical)
            {
                if (touchOffset.y > 0 && touchOffset.x >= pageTurnMeasure)
                {
                    // turn to previous page
                    GotoPage(currentPageIndex - 1);
                }
                else if (touchOffset.y < 0 && touchOffset.x <= -pageTurnMeasure)
                {
                    // turn to next page
                    GotoPage(currentPageIndex + 1);
                }
            }
        }
    }

    /// <summary>
    /// Drag the object along the plane.
    /// </summary>
    public override void Drag()
    {
        // prevent drag panel with touch
    }

    /// <summary>
    /// Gotoes the page.
    /// </summary>
    /// <param name="pageIndex">Index of the page.</param>
    public void GotoPage(int pageIndex)
    {
        int pageCount = totalPages;
        if (pageCount < 2)
            return;

        pageIndex = Mathf.Max(Mathf.Min(pageIndex, pageCount), 1);
        if (pageIndex == currentPageIndex)
            return;

        allowPageTurn = false;
        Vector3 offset = Vector3.zero;

        // turn page
        if (movement == Movement.Horizontal)
            offset = mTrans.localPosition + new Vector3(itemWidth * (currentPageIndex - pageIndex), mTrans.localPosition.y, mTrans.localPosition.z);
        else if (movement == Movement.Vertical)
            offset = mTrans.localPosition + new Vector3(mTrans.localPosition.x, itemHeight * (currentPageIndex - pageIndex), mTrans.localPosition.z);

        offset.x = Mathf.Round(offset.x);
        offset.y = Mathf.Round(offset.y);
        SpringPanel sp = SpringPanel.Begin(mPanel.gameObject, offset, 8.0f);
        sp.onFinished = new SpringPanel.OnFinished(OnPageTurnFinished);
        currentPageIndex = pageIndex;
    }

    /// <summary>
    /// Called when [page turn finished].
    /// </summary>
    private void OnPageTurnFinished()
    {
        allowPageTurn = true;
    }
}