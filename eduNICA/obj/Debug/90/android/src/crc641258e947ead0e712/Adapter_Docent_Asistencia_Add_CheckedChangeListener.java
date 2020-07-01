package crc641258e947ead0e712;


public class Adapter_Docent_Asistencia_Add_CheckedChangeListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.widget.CompoundButton.OnCheckedChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCheckedChanged:(Landroid/widget/CompoundButton;Z)V:GetOnCheckedChanged_Landroid_widget_CompoundButton_ZHandler:Android.Widget.CompoundButton/IOnCheckedChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("eduNICA.Adapter_Docent_Asistencia_Add+CheckedChangeListener, eduNICA", Adapter_Docent_Asistencia_Add_CheckedChangeListener.class, __md_methods);
	}


	public Adapter_Docent_Asistencia_Add_CheckedChangeListener ()
	{
		super ();
		if (getClass () == Adapter_Docent_Asistencia_Add_CheckedChangeListener.class)
			mono.android.TypeManager.Activate ("eduNICA.Adapter_Docent_Asistencia_Add+CheckedChangeListener, eduNICA", "", this, new java.lang.Object[] {  });
	}


	public void onCheckedChanged (android.widget.CompoundButton p0, boolean p1)
	{
		n_onCheckedChanged (p0, p1);
	}

	private native void n_onCheckedChanged (android.widget.CompoundButton p0, boolean p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
