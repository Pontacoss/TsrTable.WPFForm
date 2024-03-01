using System;
using System.Collections.Generic;
using TsrTable.Domain.Entities;

namespace TsrTable.Infrastructure.Fake
{
    public static class ParameterEntityFake
    {

        public static IReadOnlyList<ParameterEntity> GetData(int selector)
        {
            var list = new List<ParameterEntity>();
            list.Add(new ParameterEntity("客先名称"));
            list.Add(new ParameterEntity("案件名"));
            list.Add(new ParameterEntity("編成"));
            list.Add(new ParameterEntity("架線電圧"));

            return list;
        }
    }
}
