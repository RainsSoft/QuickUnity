using System.Collections;
using UnityEngine;

/// <summary>
/// This script, when attached to a panel turns it into a paged scroll view.
/// You can then attach UIDragPageView to colliders within to make it draggable.
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Page View")]
public class UIPageView : MonoBehaviour
{
    public enum Movement
    {
        Horizontal,
        Vertical
    }

    public enum DragEffect
    {
        None,
        Momentum,
        MomentumAndSpring,
    }

    /// <summary>
    /// Type of movement allowed by the page view.
    /// </summary>

    public Movement movement = Movement.Horizontal;

    /// <summary>
    /// Effect to apply when dragging.
    /// </summary>

    public DragEffect dragEffect = DragEffect.MomentumAndSpring;

    /// <summary>
    /// Create a plane on which we will be performing the dragging.
    /// </summary>

    public void Press(bool pressed)
    {
    }

    /// <summary>
    /// Drag the object along the plane.
    /// </summary>

    public void Drag()
    {
    }

    /// <summary>
    /// If the object should support the scroll wheel, do it.
    /// </summary>

    public void Scroll(float delta)
    {
    }
}