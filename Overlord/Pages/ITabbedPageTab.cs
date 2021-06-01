using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord.Pages
{
	public interface ITabbedPageTab<T>
	{

		public void Select();

		public void Deselect();

		public T Content { get; }

	}
}
