package crc641258e947ead0e712;


public class Adapter_Docent_Asistencia_Add_Guardar
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("eduNICA.Adapter_Docent_Asistencia_Add+Guardar, eduNICA", Adapter_Docent_Asistencia_Add_Guardar.class, __md_methods);
	}


	public Adapter_Docent_Asistencia_Add_Guardar ()
	{
		super ();
		if (getClass () == Adapter_Docent_Asistencia_Add_Guardar.class)
			mono.android.TypeManager.Activate ("eduNICA.Adapter_Docent_Asistencia_Add+Guardar, eduNICA", "", this, new java.lang.Object[] {  });
	}

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
