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

                //filter: {
                //    type: "select",
                //    data: datosselect[1]
                //}
            },
            {
                field: 'TareaAsignada',
                width: '7%',
                title: "Tarea asignada",
                align: 'right',
                sortable: true,
                //align: 'center',

               // formatter: formato_fecha_corta,
            },
            {
                field: 'Acciones',
                title: "Acciones Previstas para su atencion",
                width: '20%',
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
                //searchable:false,
               // filter: { type: "input" },


            },
            {
                field: 'FechaComprometida',
                title: "Fecha comprometida de cierre",
                width: '7%',
                align: 'right',
                sortable: true,
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
                align: 'center',

            },
            {
                field: "estadoTarea.EstadoTarea1",
                width: '10%',
                title: "Estado actual",
                sortable: true,
             //   formatter: formato_fecha_corta,
             //   visible: false,
                align: 'center',
            },
            {
                field: "FechaCierre",
                width: '10%',
                title: "Fecha de cierre (real)",
                sortable: true,
                //formatter: formato_fecha_corta,
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
               // formatter: operateFormatter
            }]
    });  
    $("#tareasAsignadas").bootstrapTable('resetView');
}