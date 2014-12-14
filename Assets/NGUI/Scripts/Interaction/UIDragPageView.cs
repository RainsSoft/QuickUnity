using System.Collections;
using UnityEngine;

/// <summary>
/// Allows dragging of the specified page view by mouse or touch.
/// </summary>
[AddComponentMenu("NGUI/Interaction/Drag Page View")]
public class UIDragPageView : MonoBehaviour
{
    /// <summary>
    /// Reference to the page view that will be dragged by the script.
    /// </summary>
    public UIPageView pageView;

    // Legacy functionality, kept for backwards compatibility. Use 'pageView' instead.
    [HideInInspector]
    [SerializeField]
    private UIPageView draggablePanel;

    private Transform mTrans;
    private UIPageView mScroll;
    private bool mAutoFind = false;
    private bool mStarted = false;

    /// <summary>
    /// Automatically find the page view if possible.
    /// </summary>

    private void OnEnable()
    {
        mTrans = transform;

        // Auto-upgrade
        if (pageView == null && draggablePanel != null)
        {
            pageView = draggablePanel;
            draggablePanel = null;
        }

        if (mStarted && (mAutoFind || mScroll == null))
            FindPageView();
    }

    /// <summary>
    /// Find the page view.
    /// </summary>

    private void Start()
    {
        mStarted = true;
        FindPageView();
    }

    /// <summary>
    /// Find the page view to work with.
    /// </summary>

    private void FindPageView()
    {
        // If the scroll view is on a parent, don't try to remember it (as we want it to be dynamic in case of re-parenting)
        UIPageView sv = NGUITools.FindInParents<UIPageView>(mTrans);

        if (pageView == null)
        {
            pageView = sv;
            mAutoFind = true;
        }
        else if (pageView == sv)
        {
            mAutoFind = true;
        }
        mScroll = pageView;
    }

    /// <summary>
    /// Create a plane on which we will be performing the dragging.
    /// </summary>

    private void OnPress(bool pressed)
    {
        // If the page view has been set manually, don't try to find it again
        if (mAutoFind && mScroll != pageView)
        {
            mScroll = pageView;
            mAutoFind = false;
        }

        if (pageView && enabled && NGUITools.GetActive(gameObject))
        {
            pageView.Press(pressed);

            if (!pressed && mAutoFind)
            {
                pageView = NGUITools.FindInParents<UIPageView>(mTrans);
                mScroll = pageView;
            }
        }
    }

    /// <summary>
    /// Drag the object along the plane.
    /// </summary>

    private void OnDrag(Vector2 delta)
    {
        if (pageView && NGUITools.GetActive(this))
            pageView.Drag();
    }

    /// <summary>
    /// If the object should support the scroll wheel, do it.
    /// </summary>

    private void OnScroll(float delta)
    {
        if (pageView && NGUITools.GetActive(this))
            pageView.Scroll(delta);
    }
}