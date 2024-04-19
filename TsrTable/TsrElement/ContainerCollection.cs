using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace TsrTable.RichTextBox.TsrElement
{
    public sealed class ContainerCollection : IEnumerable<Container>
    {
        public List<IContainer> containers = new List<IContainer>();

        public IEnumerator<Container> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
