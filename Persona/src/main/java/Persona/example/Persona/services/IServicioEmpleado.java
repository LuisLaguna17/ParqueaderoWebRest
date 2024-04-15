package Persona.example.Persona.services;

import Persona.example.Persona.model.Empleado;
import java.util.List;

public interface IServicioEmpleado {

    List<Empleado> getAllEmpleados();

    Empleado getEmpleadoByNumero(int numeroEmpleado);

    Empleado getEmpleadoById(String id);

    Empleado getEmpleadoByPlaca(String placa);


    void addEmpleado(Empleado empleado);

    boolean updateEmpleado(int numeroEmpleado, Empleado empleado);

    boolean deleteEmpleado(int numeroEmpleado);
}
