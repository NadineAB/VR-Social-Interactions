using UnityEngine;
using Vatio.Filters;

/*
 * This is the MonoBehaviour component of the filter.
 * It invokes a variable update every frame.
 * If You need it in FixedUpdate just change the update of the external class to FixedUpdate
 */

public class LowPassFilterMonoBehaviour : MonoBehaviour
{

    /*
     * The interface for the inner class, it exposes the Update function, so we do not need to know the filtered variable type to perform an update.
     */
    interface LowPassFilterControllerInterface
    {
        void Update();
        void SetA(float a);
    }

    /*
     * The inner class of the MonoBehaviour component.
     * It is needed due to Unity limitations, as classes deriving from MonoBehaviour cannot be generic.
     * The class itself works by taking input from the input provider object and then passing it to the filter.
     */
    class LowPassFilterController<T> : LowPassFilterControllerInterface
    {
        IFilterInput<T> lowPassFilterInput;
        LowPassFilter<T> lowPassFilter;

        /*
         * This is the Constructor for the inner class, it assigns the new input object to a variable and then instantiates the filter.
         * Parameters are:
         *      - input - the object providing input to the filter, needs to be instantiated previous to invoking this method
         *      - a - smoothing factor, the lower, the more inertia
         *      - initialValue - the initial value for the filter, it should be as close as possible to the expected average value of the filtered variable.
         */
        public LowPassFilterController(IFilterInput<T> input, float a, T initialValue)
        {
            lowPassFilterInput = input;
            lowPassFilter = new LowPassFilter<T>(a, initialValue);
        }

        /*
         * This is the method that passess the input from the input provider to the filter.
         * It is invoked periodically from the outer class.
         */
        public void Update()
        {
            lowPassFilter.Append(lowPassFilterInput.Get());
        }

        /*
         * This is the getter for the current filtered value.
         * It returns value from the filter.
         */
        public T Get()
        {
            return lowPassFilter.Get();
        }

        /*
         * This is the setter for the smoothing factor, it sets the smoothing factor of the filter.
         */
        public void SetA(float a)
        {
            lowPassFilter.A = a;
        }
    }



    LowPassFilterControllerInterface lowPassFilterController;

    /*
     * This is the function initializing auto-update by creating an instance of the inner class.
     * Parameters are:
     *      - input - the object providing input to the filter, needs to be instantiated previous to invoking this method
     *      - a - smoothing factor, the lower, the more inertia
     *      - initialValue - the initial value for the filter, it should be as close as possible to the expected average value of the filtered variable.
     */
    public void Set<T>(IFilterInput<T> input, float a, T initialValue)
    {
        lowPassFilterController = new LowPassFilterController<T>(input, a, initialValue);
    }

    /*
     * This is the getter for the current filtered value.
     * It returns value from the inner class, which in turn returns value from the filter.
     */
    public T Get<T>()
    {
        return ((LowPassFilterController<T>)lowPassFilterController).Get();
    }

    /*
     * This is the setter for the smoothing factor, it sends the new value to the inner class.
     */
    public void SetA(float a)
    {
        lowPassFilterController.SetA(a);
    }

    /*
     * This method invokes variable update in the inner class every frame.
     * If You need it in FixedUpdate, just change Update to FixedUpdate.
     */
    void Update()
    {
        lowPassFilterController.Update();
    }


}
