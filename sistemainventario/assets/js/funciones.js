// Javascript to enable link to tab
var hash = document.location.hash;
var prefix = "tab_";
if (hash) {
    $('.nav-tabs a[href="' + hash.replace(prefix, "") + '"]').tab('show');
}

// Change hash for page-reload
$('.nav-tabs a').on('shown.bs.tab', function (e) {
    window.location.hash = e.target.hash.replace("#", "#" + prefix);
});

/***********************/
function limpiarModal(modal) {
    dd = $("#modalverenlaces", modal).find("dd").toArray();
    $.each(dd, function (index, value) {
        $(value).html("");
    })
}
function datosModal(x) {
   
    var result = JSON.parse(x.result)
    var modal = "#modal_verEnlace" + result.enlacesTipo.tipo
    limpiarModal(modal);
    console.log(modal)

    $("#nroOficina",modal).html(result.oficinas.nroOficina);
    $("#nombreOficina", modal).html(result.oficinas.nombre_oficina);
    $("#tipoOficina", modal).html(result.oficinas.tipoOficina.tipo);
    $("#departamento", modal).html(result.oficinas.ciudades.departamentos.departamento);
    $("#ciudad", modal).html(result.oficinas.ciudades.ciudad);
    $("#direccion", modal).html(result.oficinas.direccion);

    var enlace = (result.enlace == 1) ? "Primario" : "Secundario";
    var contratos = "";
    if (result.contratos > 0) {
        $.each(result.contratos, function (index, value) {
            contratos += rvalue.contrato + ",";
        })
    }
    $("#enlace", modal).html(enlace)
    $("#proveedor", modal).html(result.proveedores.proveedor);
    $("#contrato", modal).html(contratos);
    $("#tecnologia", modal).html(result.enlacesTecnologia.tecnologia);
    $("#velocidad", modal).html(result.velocidad);
    $("#mensualidad", modal).html(result.mensualidad);
    /*SERVICIO*/

    if (result.enlacesServicios.length > 0) {
        $("#servicionombre", modal).html(result.enlacesServicios[0].servicio);
        $("#dirservicio", modal).html(result.enlacesServicios[0].direccion);
    }
    $("#observaciones", modal).html(result.observaciones);
    $(modal).modal("show");

}