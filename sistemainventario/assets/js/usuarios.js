function retornarTablaUsuarios() {       
   // $("#comentartarea").attr("disabled", true);
    retornarAjax(base_url("usuarios/retornarUsuarios"));
}
function mostrarTablaUsuarios(r) {
    $('#agregarUsuario').modal('hide');
    res = r.result   
    $("#listausuarios").bootstrapTable('destroy');
    $("#listausuarios").bootstrapTable({
        data: res,        
        striped: true,
        pagination: true,
        pageSize: "10",
        search: true,
        searchOnEnterKey: true,        
        showColumns: true,
        columns: [
             {
                 field: 'Nombre',
                 width: '20%',
                 title: 'Nombre',
                 align: 'left',
                 sortable: true,
             },
            {
                field: 'Usuario',
                width: '10%',
                title: 'Usuario',
                align: 'left',
                sortable: true,               
            },
            {
                field: 'Email',
                width: '20%',
                title: 'Email',
                align: 'left',
                sortable: true,
            },
            {
                field: 'Tipo',
                width: '10%',
                title: 'Tipo',
                align: 'center',
                sortable: true,
                formatter: formatoTipo,
            },
            {
                field: 'Roles',
                width: '20%',
                title: "Opciones",               
            },                                  
            {
                title: 'Acciones',
                align: 'center',
                width: '20%',
                events: operateEvents,
                formatter: operateFormatter,
            }]
    });
    $("#listausuarios").bootstrapTable('resetView');
}
window.operateEvents = {
    'click .editarTarea': function (e, value, row, index) {
        console.log(row)
        if (row.IdEstadoTarea != 2) {
            mostrarModalEditar(row);
            $("#tarticulo").bootstrapTable('hideLoading');
        }
        else {
            swal("Atencion!", "Este registro no puede ser editado", "warning")
        }

    },
    'click .verTarea': function (e, value, row, index) {

        window.location = base_url("tareas/ver/" + row.IdTarea);
    }
};

function operateFormatter(value, row, index) {
    return [        
        '<button type="button" class="btn btn-default editarUsuario" aria-label="Right Align">',
        '<i class="fa fa-pencil" aria-hidden="true"></i></button>',
        '<button type="button" class="btn btn-default eliminarUsuario" aria-label="Right Align">',
        '<i class="fa fa-trash" aria-hidden="true"></i></button>',

    ].join('');
}
function formatoTipo(value, row, index) {
    switch (value) {
        case 1:
            return "Admin";
        case 2:
            return "Usuario";
    }   
}