package crc641258e947ead0e712;


public class Fragment_Admin_Usuarios_FilterableAdapter
	extends android.widget.ArrayAdapter
	implements
		mono.android.IGCUserPeer,
		android.widget.Filterable
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getItem:(I)Ljava/lang/Object;:GetGetItem_IHandler\n" +
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"n_getFilter:()Landroid/widget/Filter;:GetGetFilterHandler\n" +
			"";
		mono.android.Runtime.register ("eduNICA.Fragment_Admin_Usuarios+FilterableAdapter, eduNICA", Fragment_Admin_Usuarios_FilterableAdapter.class, __md_methods);
	}


	public Fragment_Admin_Usuarios_FilterableAdapter (android.content.Context p0, int p1)
	{
		super (p0, p1);
		if (getClass () == Fragment_Admin_Usuarios_FilterableAdapter.class)
			mono.android.TypeManager.Activate ("eduNICA.Fragment_Admin_Usuarios+FilterableAdapter, eduNICA", "Android.Content.Context, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public Fragment_Admin_Usuarios_FilterableAdapter (android.content.Context p0, int p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == Fragment_Admin_Usuarios_FilterableAdapter.class)
			mono.android.TypeManager.Activate ("eduNICA.Fragment_Admin_Usuarios+FilterableAdapter, eduNICA", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public Fragment_Admin_Usuarios_FilterableAdapter (android.content.Context p0, int p1, int p2, java.util.List p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == Fragment_Admin_Usuarios_FilterableAdapter.class)
			mono.android.TypeManager.Activate ("eduNICA.Fragment_Admin_Usuarios+FilterableAdapter, eduNICA", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib:System.Collections.IList, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public Fragment_Admin_Usuarios_FilterableAdapter (android.content.Context p0, int p1, int p2, java.lang.Object[] p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == Fragment_Admin_Usuarios_FilterableAdapter.class)
			mono.android.TypeManager.Activate ("eduNICA.Fragment_Admin_Usuarios+FilterableAdapter, eduNICA", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib:Java.Lang.Object[], Mono.Android", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public Fragment_Admin_Usuarios_FilterableAdapter (android.content.Context p0, int p1, java.util.List p2)
	{
		super (p0, p1, p2);
		if (getClass () == Fragment_Admin_Usuarios_FilterableAdapter.class)
			mono.android.TypeManager.Activate ("eduNICA.Fragment_Admin_Usuarios+FilterableAdapter, eduNICA", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Collections.IList, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public Fragment_Admin_Usuarios_FilterableAdapter (android.content.Context p0, int p1, java.lang.Object[] p2)
	{
		super (p0, p1, p2);
		if (getClass () == Fragment_Admin_Usuarios_FilterableAdapter.class)
			mono.android.TypeManager.Activate ("eduNICA.Fragment_Admin_Usuarios+FilterableAdapter, eduNICA", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:Java.Lang.Object[], Mono.Android", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public java.lang.Object getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native java.lang.Object n_getItem (int p0);


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);


	public android.widget.Filter getFilter ()
	{
		return n_getFilter ();
	}

	private native android.widget.Filter n_getFilter ();

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
