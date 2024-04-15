package Persona.example.Persona.model;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.RequiredArgsConstructor;

@Data
@RequiredArgsConstructor
@AllArgsConstructor
@Builder
public class Empleado extends Persona{

    private Double salario;
    private int numeroEmpleado;
    private Boolean activo; //Si se encuentra trabajando o no
}
