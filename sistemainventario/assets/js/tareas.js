$(document).ready(function () {
   
    retornarAjax("tareas/retornarTareas")
    /**Iniciando datetime**/
    moment.locale("es");
    $('#fechacierre').datetimepicker({        
        format: 'DD/MM/YYYY'
    });
    
})

function mostrarTablaTareas(r)
{
    $('#agregartarea').modal('hide');
   // console.log(JSON.parse(r.result))
   // var res=JSON.parse(r.result);
    res=r.result
    console.log(res)

    $("#tareasAsignadas").bootstrapTable('destroy');
    $("#tareasAsignadas").bootstrapTable({

        data: res,
        striped: true,
        pagination: true,
        pageSize: "10",      
        search: true,        
        searchOnEnterKey: true,
        filter: true,
        showColumns: true,

        columns: [
            {
                field: 'areas.Area',
                width: '5%',
                title: 'Area',
                align: 'center',
                sortable: true,
                //filter: { type: "input" }
            },
            {
                field: 'prioridades.Prioridad',
                width: '5%',
                title: 'Prioridad',
                align: 'center',
               // visible: false,
                sortable: true,
                formatter: labelprioridad,               
            },
            {
                field: 'tipoTareas.TipoTarea',
                width: '30%',
                title: "Tipo de tarea",
                align: 'center',                                
            },
            {
                field: 'TareaAsignada',
                width: '30%',
                title: "Tarea asignada",
                align: 'center',
            },
            {
                field: 'Acciones',
                title: "Acciones previstas <br> para su atencion",
                width: '5%',
                align: 'center',
                //filter: {
                //    type: "select",
                //    data: datosselect[0]
                //},
                //sortable: true,

            },
            {
                field: 'FechaAsignacion',
                title: "<div>Fecha de</div><div> asignacion</div>",
                width: '7%',
                align: 'center',
                sortable: true,
                formatter: formato_fecha_corta,
                //searchable:false,
               // filter: { type: "input" },


            },
            {
                field: 'FechaComprometida',
                title: "Fecha <br>comprometida <br>de cierre",
                width: '7%',
                align: 'center',
                sortable: true,
                formatter: formato_fecha_corta,
                //formatter: operateFormatter3,
                //filter: { type: "input" },

            },
            {
                field: "HorasDiariasAsignadas",
                title: "Horas <br>diarias <br>asignadas",
                width: '7%',
                sortable: true,
                //filter: {
                //    type: "select",
                //    data: ["APROBADO", "PENDIENTE", "ANULADO"]
                //},
                //formatter: operateFormatter2,
                align: 'center',

            },
            {
                field: "tareaResponsable",
                width: '10%',
                title: "Responsable <br> de la tarea",
                sortable: true,
                //filter: {
                //    type: "select",
                //    data: datosselect[2]
                //},
             //   visible: false,
                formatter: responsabletarea,
                align: 'center',

            },
            {
                field: "estadoTarea.EstadoTarea1",
                width: '10%',
                title: "Estado actual",
                sortable: true,
                formatter: labelestado,
             //   visible: false,
                align: 'center',
            },
            {
                field: "FechaCierre",
                width: '10%',
                title: "Fecha de <br>cierre (real)",
                sortable: true,
                formatter: formato_fecha_corta,
                visible: false,
                align: 'center',
            },
            {
                field: "Eficiencia",
                width: '10%',
                title: "Eficiencia",
                sortable: true,
                //formatter: formato_fecha_corta,
                visible: false,
                align: 'center',

            },
            {
                title: 'Acciones',
                align: 'center',
                width: '10%',
                events: operateEvents,
                formatter: operateFormatter,
            }]
    });  
    $("#tareasAsignadas").bootstrapTable('resetView');
}
window.operateEvents = {
    'click .editarTarea': function (e, value, row, index) {
        mostrarModalEditar(row);        
        $("#tarticulo").bootstrapTable('hideLoading');
    },
    'click .verTarea': function (e, value, row, index) {
        console.log(row.IdTarea);
        window.location ="/ver";
    }
};
function operateFormatter(value, row, index) {
    return [
        '<button type="button" class="btn btn-default verTarea" aria-label="Right Align">',
        '<i class="fa fa-external-link" aria-hidden="true"></i></button>',
        '<button type="button" class="btn btn-default editarTarea" aria-label="Right Align">',
        '<i class="fa fa-pencil-square-o" aria-hidden="true"></i></button>',
        
        
    ].join('');
}
function labelprioridad(value, row, index) {
    if (value == "Alta")
        $ret = '<span class="label label-danger">ALTA</span>';
    if (value == "Media")
        $ret = '<span class="label label-warning">MEDIA</span>';
    if (value == "Baja")
        $ret = '<span class="label label-info">BAJA</span>';
    return ($ret);
}
function labelestado(value, row, index) {
    if (value == "Ejecución")
        $ret = '<span class="label label-info text-uppercase">'+value+'</span>';
    if (value == "Finalizado")
        $ret = '<span class="label label-success text-uppercase">' + value +'</span>';
    if (value == "Demorado")
        $ret = '<span class="label label-warning text-uppercase">' + value +'</span>';
    return ($ret);
}
function responsabletarea(value, row, index) {
    var $ret = "";
    $.each(value, function (index, val) {
        console.log(val.responsable.usuariosSistema);
        $ret += val.responsable.usuariosSistema.Nombre;
        $ret += "<br>";
    });
    console.log(value)
    return ($ret);
}
$(document).on("click", "#btnnuevatarea", function () {
    borrardatosModal();
})
function borrardatosModal()
{    
    $('#IdArea').selectpicker('val', "");
    $("#IdPrioridad").selectpicker('val', "");
    $("#IdTipoTarea").selectpicker('val', "");
    $("#FechaComprometida").val("");
    $("#Acciones").val("");
    $("#TareaAsignada").val("")
    $("#IdTarea").val(0)
}
function mostrarModalEditar(row)
{    
    $("#agregartarea").modal("show");
    $('#IdArea').selectpicker('val', row.IdArea);
    $("#IdPrioridad").selectpicker('val',row.IdPrioridad);
    $("#IdTipoTarea").selectpicker('val',row.IdTipoTarea);
    $("#FechaComprometida").val(formato_fecha_corta(row.FechaComprometida, null, null));
    $("#Acciones").val(row.Acciones);
    $("#TareaAsignada").val(row.TareaAsignada)
    $("#IdTarea").val(row.IdTarea)
}