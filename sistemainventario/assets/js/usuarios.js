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
                field: 'Cargo',
                width: '10%',
                title: 'Cargo',
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
$("#usuariobuscar").autocomplete({
    minLength: 2,
    source: function (request, response) {

       // $(".cargandosol").css({ "display": "block" });
        $(".usuariobuscarid").val("");
        encuentrausuario(0);
        $.ajax({
            url: base_url("Usuarios/autocompletar"),
            dataType: "json",
            data: {
                b: request.term
            },
            success: function (data) {
                response(data);
              //  $(".cargandosol").css({ "display": "none" });
            }
        });
    },
    select: function (event, ui) {
        encuentrausuario(1);
        datos = ui.item;

        $("#usuariobuscar").val(ui.item[0]);
        $("#usuariobuscarid").val(ui.item[2]);
        return false;
    }
})
    .autocomplete("instance")._renderItem = function (ul, item) {

        return $("<li>")
          .append("<a><div>" + item[0] + " </div><div class='mailage'>" + item[2] + " - " + item[1] + "</div></a>")
          .appendTo(ul);
    };function encuentrausuario(encuentra) {
    if (encuentra == 0) {
        $('#icono-busqueda').removeClass("glyphicon-ok");
        $('#icono-busqueda').addClass("glyphicon-user");
        $('#icono-busqueda-color').removeClass("coloriconook");

    }
    if (encuentra == 1) {
        $('#icono-busqueda').removeClass("glyphicon-user");
        $('#icono-busqueda').addClass("glyphicon-ok");
        $('#icono-busqueda-color').addClass("coloriconook");
    }
}
