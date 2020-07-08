using System;
using System.Drawing;
using System.Reflection;

namespace ds.test.impl
{
    public interface IPlugin
    {
        string PluginName { get; }
        string Version { get; }
        Image Image { get; }
        string Description { get; }
        int Run(int input1, int input2);
    }

    abstract class Plugin : IPlugin
    {
        public abstract string PluginName { get; }

        public abstract string Version { get; }

        public Image Image => Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ds.test.impl.Plugin.png"));

        public virtual string Description => "Plugin Description : ";

        public abstract int Run(int input1, int input2);

    }

    /// <summary>
    /// Применяется для сложения чисел
    /// </summary>
    class AddPlugin : Plugin
    {
        public override string PluginName => nameof(AddPlugin);

        public override string Version => "0.0.0.1";

        public override string Description => base.Description + "Сложу два числа";

        /// <summary>
        /// Складывает значения input1 и input2. Выдаст ошибку переполнения в случае переполнения
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public override int Run(int input1, int input2)
        {            
            long result = (long)input1 + (long)input2;
            if (result < int.MinValue || result > int.MaxValue)
                throw new OverflowException("Значение было слишком малым, либо слишком большим для int");
            return (int)result;
        }
    }

    /// <summary>
    /// Умножит значения input1 и input2. Выдаст ошибку переполнения в случае переполнения
    /// </summary>
    class MultiplyPlugin : Plugin
    {
        public override string PluginName => nameof(MultiplyPlugin);
        public override string Version => "0.0.0.1";

        public override string Description => base.Description + "Умножу два числа";

        /// <summary>
        ///  Умножит значения input1 и input2
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public override int Run(int input1, int input2)
        {
            long result = (long)input1 * (long)input2;
            if (result < int.MinValue || result > int.MaxValue)
                throw new OverflowException("Значение было слишком малым, либо слишком большим для int");
            return (int)result;
        }
    }

    /// <summary>
    /// Применяется для разделения чисел без округления
    /// </summary>
    class DividePlugin : Plugin
    {
        public override string PluginName => nameof(DividePlugin);
        public override string Version => "0.0.0.1";

        public override string Description => base.Description + "Разделю первое число на второе. Если результат нужно будет округлить, то выкину ошибку";

        /// <summary>
        /// Разделит значение input1 на input2. Выдаст ошибку потери точности в случае не целого деления
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public override int Run(int input1, int input2)
        {
            double result = (double)input1 / (double)input2;
            if (result % 1 != 0)
                throw new Exception("Потеря точности");


            return input1 / input2;
        }
    }

    /// <summary>
    /// Применяется для разделения чисел с округлением
    /// </summary>
    class DivideWithRoundPlugin : Plugin
    {
        public override string PluginName => nameof(DivideWithRoundPlugin);
        public override string Version => "0.0.0.1";

        public override string Description => base.Description + "Разделю первое число на второе и округлю результат в случае надобности";

        /// <summary>
        /// Разделит значение input1 на input2. Не выдаст ошибку потери точности в случае не целого деления
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public override int Run(int input1, int input2)
        {
            return input1 / input2;
        }
    }

    /// <summary>
    /// Применяется для возведения числа в степень
    /// </summary>
    class PowPlugin : Plugin
    {
        public override string PluginName => nameof(PowPlugin);
        public override string Version => "0.0.0.1";

        public override string Description => base.Description + "Возведу первое число в степень, указанную в втором числе";

        /// <summary>
        /// Возведёт значение input1 в степень input2. Выдаст ошибку если значение input2 будет меньше 0.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public override int Run(int input1, int input2)
        {
            long result = 1;
            // Если 1 возводим в степень
            if (input1 == 1)
                return 1;
            // Если возводим в 0 степень
            if (input2 == 0)
                return 1;
            if (input2 < 0)
            {
                throw new Exception("Значение int не может быть дробным");
            }
            // Возведение в степень
            for (int i = 0; i < input2; i++)
            {
                result = (long)result * (long)input1;
                if (result < int.MinValue || result > int.MaxValue)
                    throw new OverflowException("Значение было слишком малым, либо слишком большим для int");
            }
            return (int)result;
        }
    }

    /// <summary>
    /// Поможет экипажу найти ответ на главный вопрос жизни, вселенной и всего такого.
    /// </summary>
    class UltimateAnswerOfLifeAndUniverseAndEverythingPlugin : Plugin
    {
        public override string PluginName => "Deep Thought";

        public override string Version => "Ended Version";

        public override string Description => base.Description + "Если вдруг вы в космосе ищете ответ на главный вопрос жизни, вселенной и всего такого, то воспользуйтесь этим плагином";

        /// <summary>
        /// Ответ может всех огорчить. Вернёт значение 42
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public override int Run(int input1, int input2)
        {
            return 42;
        }
    }
}
