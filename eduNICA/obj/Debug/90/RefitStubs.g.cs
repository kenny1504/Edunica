﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using eduNICA.RefitInternalGenerated;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace eduNICA.RefitInternalGenerated
{
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8669 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.
namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Admin_home : Interface_Admin_home
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Admin_home(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Estudiantes_grados_Admin>> Interface_Admin_home.Total_Grados()
        {
            var arguments = new object[] {  };
            var func = requestBuilder.BuildRestResultFuncForMethod("Total_Grados", new Type[] {  });
            return (Task<List<Estudiantes_grados_Admin>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Add_User : Interface_Instit_Add_User
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Add_User(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<usuariosWS> Interface_Instit_Add_User.Registro_Usuario_Docente(Usuarios docent)
        {
            var arguments = new object[] { docent };
            var func = requestBuilder.BuildRestResultFuncForMethod("Registro_Usuario_Docente", new Type[] { typeof(Usuarios) });
            return (Task<usuariosWS>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_home : Interface_Instit_home
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_home(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Estudiantes_grados_grafico>> Interface_Instit_home.Total_Grados(Busqueda inst)
        {
            var arguments = new object[] { inst };
            var func = requestBuilder.BuildRestResultFuncForMethod("Total_Grados", new Type[] { typeof(Busqueda) });
            return (Task<List<Estudiantes_grados_grafico>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Lista_Usuario_Docente : Interface_Instit_Lista_Usuario_Docente
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Lista_Usuario_Docente(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<usuariosWS>> Interface_Instit_Lista_Usuario_Docente.Usuarios_Docentes(Busqueda inst)
        {
            var arguments = new object[] { inst };
            var func = requestBuilder.BuildRestResultFuncForMethod("Usuarios_Docentes", new Type[] { typeof(Busqueda) });
            return (Task<List<usuariosWS>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Lista_Usuario_Docente_Detalle : Interface_Instit_Lista_Usuario_Docente_Detalle
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Lista_Usuario_Docente_Detalle(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Personas>> Interface_Instit_Lista_Usuario_Docente_Detalle.Datos_Docente(BusquedaUD u)
        {
            var arguments = new object[] { u };
            var func = requestBuilder.BuildRestResultFuncForMethod("Datos_Docente", new Type[] { typeof(BusquedaUD) });
            return (Task<List<Personas>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Matricula_Grado : Interface_Instit_Matricula_Grado
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Matricula_Grado(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Estudiantes_grados>> Interface_Instit_Matricula_Grado.Estudiante_Grado(Busqueda inst)
        {
            var arguments = new object[] { inst };
            var func = requestBuilder.BuildRestResultFuncForMethod("Estudiante_Grado", new Type[] { typeof(Busqueda) });
            return (Task<List<Estudiantes_grados>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Matricula_Grados_Grupo : Interface_Instit_Matricula_Grados_Grupo
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Matricula_Grados_Grupo(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<grupos_grados>> Interface_Instit_Matricula_Grados_Grupo.Estudiante_Grado_Grupo(Grupos_ws inst)
        {
            var arguments = new object[] { inst };
            var func = requestBuilder.BuildRestResultFuncForMethod("Estudiante_Grado_Grupo", new Type[] { typeof(Grupos_ws) });
            return (Task<List<grupos_grados>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Matricula_Grados_Grupo_Estudiante : Interface_Instit_Matricula_Grados_Grupo_Estudiante
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Matricula_Grados_Grupo_Estudiante(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<ListaEstudiantesWS>> Interface_Instit_Matricula_Grados_Grupo_Estudiante.Estudiantes_Institucion(estudianteWS dato)
        {
            var arguments = new object[] { dato };
            var func = requestBuilder.BuildRestResultFuncForMethod("Estudiantes_Institucion", new Type[] { typeof(estudianteWS) });
            return (Task<List<ListaEstudiantesWS>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Matricula_Grados_Grupo_Estudiante_Detalle : Interface_Instit_Matricula_Grados_Grupo_Estudiante_Detalle
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Matricula_Grados_Grupo_Estudiante_Detalle(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<DatosWS>> Interface_Instit_Matricula_Grados_Grupo_Estudiante_Detalle.Datos_Institucion(estudianteWS dato)
        {
            var arguments = new object[] { dato };
            var func = requestBuilder.BuildRestResultFuncForMethod("Datos_Institucion", new Type[] { typeof(estudianteWS) });
            return (Task<List<DatosWS>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Nota_G_G_Asignatura : Interface_Instit_Nota_G_G_Asignatura
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Nota_G_G_Asignatura(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Asignaturas>> Interface_Instit_Nota_G_G_Asignatura.Materias(estudianteWS dato)
        {
            var arguments = new object[] { dato };
            var func = requestBuilder.BuildRestResultFuncForMethod("Materias", new Type[] { typeof(estudianteWS) });
            return (Task<List<Asignaturas>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Nota_G_G_detallenota : Interface_Instit_Nota_G_G_detallenota
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Nota_G_G_detallenota(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Detallenota>> Interface_Instit_Nota_G_G_detallenota.Detallenota()
        {
            var arguments = new object[] {  };
            var func = requestBuilder.BuildRestResultFuncForMethod("Detallenota", new Type[] {  });
            return (Task<List<Detallenota>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_Instit_Nota_G_G_VerNotaEstudiante : Interface_Instit_Nota_G_G_VerNotaEstudiante
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_Instit_Nota_G_G_VerNotaEstudiante(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Notas_Estudiante>> Interface_Instit_Nota_G_G_VerNotaEstudiante.notas_Estudiantes(BusquedaNota dato)
        {
            var arguments = new object[] { dato };
            var func = requestBuilder.BuildRestResultFuncForMethod("notas_Estudiantes", new Type[] { typeof(BusquedaNota) });
            return (Task<List<Notas_Estudiante>>)func(Client, arguments);
        }
    }
}

namespace eduNICA.Resources.Intarface
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Runtime;
    using global::Android.Views;
    using global::Android.Widget;
    using global::eduNICA.Resources.Model;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedInterface_LoginInterface : Interface_LoginInterface
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedInterface_LoginInterface(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<usuariosview>> Interface_LoginInterface.Autenticar(userview user)
        {
            var arguments = new object[] { user };
            var func = requestBuilder.BuildRestResultFuncForMethod("Autenticar", new Type[] { typeof(userview) });
            return (Task<List<usuariosview>>)func(Client, arguments);
        }
    }
}

#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS8669 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.
