using System;
using System.Collections.Generic;
using System.Text;
using Hiro.Containers;
using Tao.Interfaces;

namespace Tao.Core
{
    public class Builder<TInput>
    {
        private readonly IFactory<TInput> _inputFactory;
        public Builder(IFactory<TInput> inputFactory)
        {
            _inputFactory = inputFactory;
        }

        public TFactoryOutput CreateFrom<TFactoryOutput>(IConversion<TInput, TFactoryOutput> factory)
        {
            var input = _inputFactory.Create();
            var result = factory.Create(input);

            return result;
        }
    }
}
