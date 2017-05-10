$(document).ready(function () {
    retornarAjax("tareas/retornarTareas")
})
function mostrarTablaTareas(r)
{
    console.log(JSON.parse(r.result))
    var res=JSON.parse(r.result);
    console.log(res)

    $("#tareasAsignadas").bootstrapTable('destroy');
    $("#tareasAsignadas").bootstrapTable({

        data: res,
        striped: true,
        pagination: true,
        pageSize: "100",      
        search: true,        
        searchOnEnterKey: true,
        filter: true,
        showColumns: true,

        columns: [
            {
                field: 'areas.Area',
                width: '50%',
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
                field: 'TareaAsignada',
                width: '30%',
                title: "Tarea asignada",
                align: 'right',
                
                //align: 'center',

               // formatter: formato_fecha_corta,
            },
            {
                field: 'Acciones',
                title: "Acciones Previstas para su atencion",
                width: '5%',
                //filter: {
                //    type: "select",
                //    data: datosselect[0]
                //},
                //sortable: true,

            },
            {
                field: 'FechaAsignacion',
                title: "Fecha de asignacion",
                width: '7%',
                sortable: true,
                formatter: formato_fecha_corta,
                //searchable:false,
               // filter: { type: "input" },


            },
            {
                field: 'FechaComprometida',
                title: "Fecha comprometida de cierre",
                width: '7%',
                align: 'right',
                sortable: true,
                formatter: formato_fecha_corta,
                //formatter: operateFormatter3,
                //filter: { type: "input" },

            },
            {
                field: "HorasDiariasAsignadas",
                title: "Horas diarias asignadas",
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
                title: "Responsable de la tarea",
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
                title: "Fecha de cierre (real)",
                sortable: true,
                formatter: formato_fecha_corta,
             //   visible: false,
                align: 'center',

            },
            {
                field: "Eficiencia",
                width: '10%',
                title: "Eficiencia",
                sortable: true,
                //formatter: formato_fecha_corta,
             //   visible: false,
                align: 'center',

            },
            {
                title: 'Acciones',
                align: 'center',
                width: '10%',
              //  events: operateEvents,
                formatter: operateFormatter,
            }]
    });  
    $("#tareasAsignadas").bootstrapTable('resetView');
}
function operateFormatter(value, row, index) {
    return [
        '<button type="button" class="btn btn-default verIngreso" aria-label="Right Align">',
        '<span class="fa fa-external-link" aria-hidden="true"></span></button>',
        
        
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