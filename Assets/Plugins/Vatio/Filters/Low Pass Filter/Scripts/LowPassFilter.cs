/*
 * This is the main filter class. The filter works by averaging current data with past values
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Vatio.Filters
{
    public class LowPassFilter<T>
    {
        delegate object DoFilter(object value);
        DoFilter innerFilter;

        /*
         * This is the smoothing factor, with getter and setter
         */
        protected float a;
        public float A
        {
            get { return a; }
            set { a = value; }
        }

        /*
         * This is the current filtered value
         */
        protected T avg;

        // Types that require special handling
        enum SpecialTypes { stbyte, stsbyte, stshort, stushort, stint, stuint, stlong, stulong, stfloat, stdouble, stdecimal, stQuaternion };

        static Dictionary<Type, SpecialTypes> specialTypesMapping = new Dictionary<Type, SpecialTypes>() { 
            {typeof(byte), SpecialTypes.stbyte},
            {typeof(sbyte), SpecialTypes.stsbyte},
            {typeof(short), SpecialTypes.stshort},
            {typeof(ushort), SpecialTypes.stushort},
            {typeof(int), SpecialTypes.stint},
            {typeof(uint), SpecialTypes.stuint},
            {typeof(long), SpecialTypes.stlong},
            {typeof(ulong), SpecialTypes.stulong},
            {typeof(float), SpecialTypes.stfloat},
            {typeof(double), SpecialTypes.stdouble},
            {typeof(decimal), SpecialTypes.stdecimal},
            {typeof(Quaternion), SpecialTypes.stQuaternion}
        };

        /*
         * These are methods references when filtering "other" or non-special variable types
         */
        static MethodInfo add, subtract, multiplyTF;

        /*
         * This is the static constructor for the LowPassFilter class.
         * It is called once per class upon engine initialization.
         * It assigns methods to the references above via reflections for "other" or non-special variable types.
         */
        static LowPassFilter()
        {
            if (specialTypesMapping.ContainsKey(typeof(T)))
                return;

            try
            {
                add = typeof(T).GetMethod("op_Addition", new System.Type[] { typeof(T), typeof(T) });
                subtract = typeof(T).GetMethod("op_Subtraction", new System.Type[] { typeof(T), typeof(T) });
                multiplyTF = typeof(T).GetMethod("op_Multiply", new System.Type[] { typeof(T), typeof(float) });
            }
            catch (System.Reflection.AmbiguousMatchException)
            {
                // This exception should not happen, as all the methods are provided with the exact parameters, but to satisfy Unity Asset Store submission guidelines it is handled.
                Debug.Log("Error: the class You are using in the filter has multiple implementations of handling operators - please use different class");
                throw;
            }

            if (add == null)
            {
                Debug.Log("Error: the class You are using does not handle '+' operator - either change the class implementation, or use a different class");
                throw new System.InvalidOperationException("Supplied data type does not support '+' operator");
            }

            if (subtract == null)
            {
                Debug.Log("Error: the class You are using does not handle '-' operator - either change the class implementation, or use a different class");
                throw new System.InvalidOperationException("Supplied data type does not support '-' operator");
            }

            if (multiplyTF == null)
            {
                Debug.Log("Error: the class You are using does not handle '*' operator between itself and a float value - either change the class implementation, or use a different class");
                throw new System.InvalidOperationException("Supplied data type does not support '*' operator between itself and a float");
            }

        }

        /*
        * This is the constructor for the filter. It checks what class should handle the supplied data type. If it is one of the generics it will be handled by the appropriate class, if not it will be handled by the default class.
        * Parameters are:
        *      a - alpha parameter, the lower the value, the more inertia filter has, it has to be in range [0, 1]
        *      initialValue - initial value for the filter, it should be set as close as possible to expected average value of the filtered variable
        */
        public LowPassFilter(float a, T initialValue)
        {
            this.a = Mathf.Clamp(a, 0.0f, 1.0f);
            if (initialValue == null)
            {
                Debug.Log("Error: supplied initial value for Low Pass Filter is null - please provide a correct value, that is as close as possible to expected average value of the filtered variable");
                throw new System.ArgumentNullException("Low Pass Filter initial value cannot be null");
            }
            avg = initialValue;

            if (specialTypesMapping.ContainsKey(typeof(T)))
            {
                switch (specialTypesMapping[typeof(T)])
                {
                    case SpecialTypes.stbyte:
                        innerFilter = AppendByte;
                        break;
                    case SpecialTypes.stsbyte:
                        innerFilter = AppendSbyte;
                        break;
                    case SpecialTypes.stshort:
                        innerFilter = AppendShort;
                        break;
                    case SpecialTypes.stushort:
                        innerFilter = AppendUshort;
                        break;
                    case SpecialTypes.stint:
                        innerFilter = AppendInt;
                        break;
                    case SpecialTypes.stuint:
                        innerFilter = AppendUint;
                        break;
                    case SpecialTypes.stlong:
                        innerFilter = AppendLong;
                        break;
                    case SpecialTypes.stulong:
                        innerFilter = AppendUlong;
                        break;
                    case SpecialTypes.stfloat:
                        innerFilter = AppendFloat;
                        break;
                    case SpecialTypes.stdouble:
                        innerFilter = AppendDouble;
                        break;
                    case SpecialTypes.stdecimal:
                        innerFilter = AppendDecimal;
                        break;
                    case SpecialTypes.stQuaternion:
                        innerFilter = AppendQuaternion;
                        break;
                    default:
                        innerFilter = AppendOther;
                        break;
                }
            }
            else
            {
                innerFilter = AppendOther;
            }
        }

        /*
         * This is the function adding new generic unfiltered value for the filter
         * Parameters are:
         *      input - the new value
         *      
         * The function returns the new filtered value
         */
        public T Append(T input)
        {
            return (T)innerFilter(input);
        }

        /*
         * This is the getter for current filtered value
         */
        public T Get()
        {
            return avg;
        }

        /*
         * This method handles the filtering when the value type is byte
         */
        public object AppendByte(object v)
        {
            byte input = (byte)v;
            byte lavg = (byte)(object)avg;
            avg = (T)(object)(byte)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is sbyte
         */
        public object AppendSbyte(object v)
        {
            sbyte input = (sbyte)v;
            sbyte lavg = (sbyte)(object)avg;
            avg = (T)(object)(sbyte)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is short
         */
        public object AppendShort(object v)
        {
            short input = (short)v;
            short lavg = (short)(object)avg;
            avg = (T)(object)(short)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is ushort
         */
        public object AppendUshort(object v)
        {
            ushort input = (ushort)v;
            ushort lavg = (ushort)(object)avg;
            avg = (T)(object)(ushort)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is int
         */
        public object AppendInt(object v)
        {
            int input = (int)v;
            int lavg = (int)(object)avg;
            avg = (T)(object)(int)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is uint
         */
        public object AppendUint(object v)
        {
            uint input = (uint)v;
            uint lavg = (uint)(object)avg;
            avg = (T)(object)(uint)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is long
         */
        public object AppendLong(object v)
        {
            long input = (long)v;
            long lavg = (long)(object)avg;
            avg = (T)(object)(long)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is ulong
         */
        public object AppendUlong(object v)
        {
            ulong input = (ulong)v;
            ulong lavg = (ulong)(object)avg;
            avg = (T)(object)(ulong)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is float
         */
        public object AppendFloat(object v)
        {
            float input = (float)v;
            float lavg = (float)(object)avg;
            avg = (T)(object)(float)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is double
         */
        public object AppendDouble(object v)
        {
            double input = (double)v;
            double lavg = (double)(object)avg;
            avg = (T)(object)(double)(lavg + a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is decimal
         */
        public object AppendDecimal(object v)
        {
            decimal input = (decimal)v;
            decimal lavg = (decimal)(object)avg;
            avg = (T)(object)(decimal)(lavg + (decimal)a * (input - lavg));

            return avg;
        }

        /*
         * This method handles the filtering when the value type is Quaternion
         */
        public object AppendQuaternion(object v)
        {
            Quaternion input = (Quaternion)v;
            Quaternion lavg = (Quaternion)(object)avg;
            avg = (T)(object)Quaternion.Lerp(lavg, input, a);

            return avg;
        }

        /*
         * This method handles the filtering when the value type is not handled by any other method
         */
        public object AppendOther(object v)
        {
            T input = (T)v;

            T var1 = (T)subtract.Invoke(null, new object[] { input, avg });
            T var2 = (T)multiplyTF.Invoke(null, new object[] { var1, a });
            avg = (T)add.Invoke(null, new object[] { var2, avg });

            return avg;
        }


    }
}