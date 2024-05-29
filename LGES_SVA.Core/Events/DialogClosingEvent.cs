using Prism.Events;
using System.ComponentModel;

namespace LGES_SVA.Core.Events
{
	public class DialogClosingEvent : PubSubEvent<CancelEventArgs>
	{
	}
}
