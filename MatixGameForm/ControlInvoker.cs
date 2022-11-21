using System;
using System.Collections;
using System.Windows.Forms;


namespace MatixGameForm
{
	public delegate void MethodCallInvoker (object[] o);

	// Control.Invoke allows a method to be invoked on the same thread as the one
	// the control was created on.  Unlike in the full .NET Framework, the .NET
	// Compact Framework does not support the Control.Invoke overload for passing an 
	// array of objects to pass as arguments.  This ControlInvoker class overcomes
	// this limitation.


	public class ControlInvoker
	{
		private class MethodCall 
		{
			public MethodCallInvoker invoker;
			public object[] arguments;

			public MethodCall (MethodCallInvoker invoker, object[] arguments) 
			{
				this.invoker = invoker;
				this.arguments = arguments;
			}
		}

		private Control control;
		private Queue argumentInvokeList = new Queue ();

		// The constructor typically takes a form
		public ControlInvoker (Control control) 
		{
			this.control = control;
		}

		// The delegate wrapping the method and its arguments 
		// to be called on the same thread as the control.
		public void Invoke (MethodCallInvoker invoker, params object[] arguments) 
		{
			this.argumentInvokeList.Enqueue (new MethodCall (invoker, arguments));
			control.Invoke (new EventHandler (ControlInvoke));
		}

		private void ControlInvoke (object sender, EventArgs e) 
		{
			MethodCall methodCall = (MethodCall) argumentInvokeList.Dequeue();
			methodCall.invoker (methodCall.arguments);
		}
	}

}
