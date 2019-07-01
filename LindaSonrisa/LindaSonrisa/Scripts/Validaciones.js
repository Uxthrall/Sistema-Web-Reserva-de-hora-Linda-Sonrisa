function InicioSesion() {
    var email = document.getElementById('Email').value;
    var password = document.getElementById('password').value;

    if (email == "" || password == "") {
        swal("Campos Vacios", "Los campos son obligatorio", "error");
        return false;
    }

}

function CrearCuenta() {
    var email = document.getElementById('Email').value;
    var Password = document.getElementById('Password').value;
    var Password2 = document.getElementById('Password2').value;
    var rut = document.getElementById('rut').value;
    var dv_rut = document.getElementById('dv_rut').value;
    var Name = document.getElementById('Name').value;
    var p_apellido = document.getElementById('p_apellido').value;
    var s_apellido = document.getElementById('s_apellido').value;
    var Celular = document.getElementById('Celular').value;
    var Direccion = document.getElementById('Direccion').value;
    var n_direccion = document.getElementById('n_direccion').value;
    var Ciudad = document.getElementById('Ciudad').value;
    var Comuna = document.getElementById('Comuna').value;

    var email2 = (email.length > 60) ? "Campo EMAIL, " : "";
    var Password3 = (Password.length > 20) ? "Campo CONTRASEÑA, " : "";
    var Password4 = (Password2.length > 20) ? "Campo CONTRASEÑA DOS, " : "";
    var rut2 = (rut.length > 8 || rut.length < 7) ? "Campo RUT, " : "";
    var dv_rut2 = (dv_rut.length != 1) ? "Campo DV RUT, " : "";
    var Name2 = (Name.length > 25) ? "Campo NOMBRE, " : "";
    var p_apellido2 = (p_apellido.length > 30) ? "Campo P APELLIDO, " : "";
    var s_apellido2 = (s_apellido.length > 30) ? "Campo S APELLIDO, " : "";
    var Celular2 = (Celular.length > 9) ? "Campo CELULAR, " : "";
    var Direccion2 = (Direccion.length > 60) ? "Campo DIRECCION, " : "";
    var n_direccion2 = (n_direccion.length > 6) ? "Campo N° DIRECCION, " : "";
    var Ciudad2 = (Ciudad.length > 60) ? "Campo CIUDAD, " : "";
    var Comuna2 = (Comuna.length > 40) ? "Campo COMUNA, " : "";

    if (email == "" || Password == "" || Password2 == "" || rut == "" || dv_rut == "" || Name == "" || p_apellido == "" || s_apellido == "" ||
        Celular == "" || Direccion == "" || n_direccion == "" || Ciudad == "" || Comuna == "") {
        swal("Campos Vacios", "Los campos son obligatorio", "error");
        return false;
    }
    else if (Password != Password2) {
        swal("Contraseña", "La Contraseña no Coinciden", "error");
        return false;
    }
    else if (email.length > 60 || Password.length > 20 || Password2.length > 20 || rut.length > 8 || rut.length < 7 || dv_rut.length != 1 || Name.length > 25 ||
        p_apellido.length > 30 || s_apellido.length > 30 || Celular.length > 9 || Direccion.length > 60 || n_direccion.length > 6
        || Ciudad.length > 60 || Comuna.length > 40) {
        swal("Maximo de largo", "En El/Los Campo: \n" + email2 + "\n" + Password3 + "\n" + Password4 + "\n" + rut2 + "\n" + dv_rut2 + "\n" + Name2 + "\n" + p_apellido2 + "\n" + s_apellido2 + "\n" + Direccion2 + "\n" + Celular2 + "\n" + n_direccion2 + "\n" + Ciudad2 + "\n" + Comuna2 + "\n", "error");
        return false;
    }
}

function SeleccionFecha() {
    var day = document.getElementById("day").value;

    if (day == "") {
        swal("Campos Vacios Obligatorio", "Seleccione dia a buscar", "error");
        return false;
    }
}

function confirmacion() {
    var rut_apo = document.getElementById('rut_apo').value;
    var nombre_paciente = document.getElementById('nombre_paciente').value;
    var nom_apoderado = document.getElementById('nom_apoderado').value;
    var p_apellido = document.getElementById('p_apellido').value;
    var s_apellido = document.getElementById('s_apellido').value;
    var Email = document.getElementById('Email').value;
    var Celular = document.getElementById('Celular').value;


    if (rut_apo == "" || nombre_paciente==""|| nom_apoderado == "" || p_apellido == "" || s_apellido == "" || Email == "" || Celular == "") {
        swal("Campos Vacios", "Los campos son obligatorio", "error");
        return false;
    }
    else if (nombre_paciente.length > 60) {
        swal("Campos Maximo de lo Permitido", "El campo Paciente no debe exceder de lo permitido", "error");
        return false;
    }
}