package Persona.example.Persona.controller;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import Persona.example.Persona.model.Empleado;
import Persona.example.Persona.services.ErrorMessage;
import Persona.example.Persona.services.IServicioEmpleado;

import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@RestController
@RequestMapping(value = "/empleados")
public class EmpleadoController {

    @Autowired
    private IServicioEmpleado servicioEmpleado;

    @GetMapping(value = "/healthcheck")
    public String healthCheck(){
        return "Service status fine!";
    }

    @GetMapping
    public ResponseEntity<List<Empleado>> getEmpleados() {
        try {
            List<Empleado> empleados = servicioEmpleado.getAllEmpleados();
            if (empleados.isEmpty()) {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "No se encontraron empleados");
            }
            return ResponseEntity.ok(empleados);
        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error al obtener empleados", e);
        }
    }

    @GetMapping("/{tipoBusqueda}/{valorBusqueda}")
    public ResponseEntity<Empleado> getEmpleadoByTipo(@PathVariable String tipoBusqueda, @PathVariable String valorBusqueda) {
        try {
            Empleado empleado = null;
            if (tipoBusqueda.equalsIgnoreCase("numero")) {
                int numeroEmpleado = Integer.parseInt(valorBusqueda);
                empleado = servicioEmpleado.getEmpleadoByNumero(numeroEmpleado);
            } else if (tipoBusqueda.equalsIgnoreCase("id")) {
                empleado = servicioEmpleado.getEmpleadoById(valorBusqueda);
            } else if (tipoBusqueda.equalsIgnoreCase("placa")) {
                empleado = servicioEmpleado.getEmpleadoByPlaca(valorBusqueda);
            }

            if (empleado == null) {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Empleado no encontrado");
            }

            return ResponseEntity.ok().body(empleado);
        } catch (NumberFormatException e) {
            throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "El valor de búsqueda no es válido");
        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error al obtener el empleado", e);
        }
    }



    @PostMapping
    public ResponseEntity<Empleado> setEmpleado(@RequestBody Empleado empleado, BindingResult result) {
        try {
            if (result.hasErrors()) {
                throw new ResponseStatusException(HttpStatus.BAD_REQUEST, this.formatMessage(result));
            }

            empleado.setHoraLlegada(LocalDateTime.now());
            servicioEmpleado.addEmpleado(empleado);
            return ResponseEntity.status(HttpStatus.CREATED).body(empleado);
        } catch (RuntimeException e) {
            // Captura la excepción de empleado existente y devuelve una respuesta de conflicto
            throw new ResponseStatusException(HttpStatus.CONFLICT, e.getMessage(), e);
        }
    }

    @PutMapping(value = "/{numeroEmpleado}")
    public ResponseEntity<Empleado> updateEmpleado(@RequestBody Empleado empleado,
                                                   @PathVariable("numeroEmpleado") int numeroEmpleado, BindingResult result) {
        try {
            if (result.hasErrors()) {
                throw new ResponseStatusException(HttpStatus.BAD_REQUEST, this.formatMessage(result));
            }
            boolean updated = servicioEmpleado.updateEmpleado(numeroEmpleado, empleado);
            if (updated) {
                return ResponseEntity.ok().body(empleado);
            } else {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Empleado no encontrado");
            }
        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error al actualizar empleado", e);
        }
    }

    @DeleteMapping(value = "/{numeroEmpleado}")
    public ResponseEntity<String> deleteEmpleado(@PathVariable("numeroEmpleado") int numeroEmpleado) {
        try {
            boolean deleted = servicioEmpleado.deleteEmpleado(numeroEmpleado);
            if (deleted) {
                return ResponseEntity.ok().body("Empleado eliminado exitosamente");
            } else {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Empleado no encontrado");
            }
        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error al eliminar empleado", e);
        }
    }



    private String formatMessage(BindingResult result){
        List<Map<String,String>> errores = result.getFieldErrors().stream()
                .map(err -> {
                    Map<String,String> error = new HashMap<>();
                    error.put(err.getField(), err.getDefaultMessage());
                    return error;
                }).collect(Collectors.toList());

        ErrorMessage errorMessage = ErrorMessage.builder()
                .code("01")
                .mensajes(errores)
                .build();

        ObjectMapper mapper = new ObjectMapper();
        String jsonString = "";
        try {
            jsonString = mapper.writeValueAsString(errorMessage);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }

        return jsonString;
    }
}
