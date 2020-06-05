package crc641258e947ead0e712;


public class Activity_Instit_Matricula_Grado_Estudiante_Detalle
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("eduNICA.Activity_Instit_Matricula_Grado_Estudiante_Detalle, eduNICA", Activity_Instit_Matricula_Grado_Estudiante_Detalle.class, __md_methods);
	}


	public Activity_Instit_Matricula_Grado_Estudiante_Detalle ()
	{
		super ();
		if (getClass () == Activity_Instit_Matricula_Grado_Estudiante_Detalle.class)
			mono.android.TypeManager.Activate ("eduNICA.Activity_Instit_Matricula_Grado_Estudiante_Detalle, eduNICA", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
