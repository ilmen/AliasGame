using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliasGameBL.Utillity
{
    public class StringCutter : ICutter<string>
    {
        public string[] CutMultipleOfBasis(string[] entities, int basis)
        {
            if (entities == null) throw new ArgumentNullException("Ссылка на список слов не передана!");
            if (entities.Length < basis) throw new ArgumentOutOfRangeException("Слов слишком мало даже для одной группы! Минимум: " + basis);
            if (basis < 1) throw new ArgumentException("Базис должен быть натуральным числом!");

            // усекаем кол-во слов так, чтобы получилось целое число карточек
            var cardCount = (int)(entities.Length / basis);
            return entities.Take(cardCount * basis).ToArray();
        }
    }
}
