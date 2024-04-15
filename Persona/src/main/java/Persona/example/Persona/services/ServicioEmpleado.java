package Persona.example.Persona.services;

import Persona.example.Persona.model.Empleado;
import org.springframework.stereotype.Service;
import java.util.ArrayList;
import java.util.List;

@Service
public class ServicioEmpleado implements IServicioEmpleado {

    private List<Empleado> empleados = new ArrayList<>();

    @Override
    public List<Empleado> getAllEmpleados() {
        return empleados;
    }

    @Override
    public Empleado getEmpleadoByNumero(int numeroEmpleado) {
        for (Empleado empleado : empleados) {
            if (empleado.getNumeroEmpleado() == numeroEmpleado) {
                return empleado;
            }
        }
        return null; // Empleado no encontrado
    }

    @Override
    public Empleado getEmpleadoById(String id) {
        for (Empleado empleado : empleados) {
            if (empleado.getId().equals(id)) {
                return empleado;
            }
        }
        return null; // Empleado no encontrado
    }

    @Override
    public Empleado getEmpleadoByPlaca(String placa) {
        for (Empleado empleado : empleados) {
            if (empleado.getPlacaVehiculo().equals(placa)) {
                return empleado;
            }
        }
        return null; // Empleado no encontrado
    }


    @Override
    public void addEmpleado(Empleado empleado) {
        // Verificar si ya existe un empleado con el mismo número, ID o placa
        if (empleadoExists(empleado.getNumeroEmpleado(), empleado.getId(), empleado.getPlacaVehiculo())) {
            throw new RuntimeException("Este empleado ya existe");
        }
        // Si no existe, agregar el empleado
        empleados.add(empleado);
    }

    private boolean empleadoExists(int numeroEmpleado, String id, String placa) {
        // Verificar si hay un empleado con el mismo número, ID o placa
        for (Empleado empleado : empleados) {
            if (empleado.getNumeroEmpleado() == numeroEmpleado || empleado.getId().equals(id) || empleado.getPlacaVehiculo().equals(placa)) {
                return true;
            }
        }
        return false;
    }

    @Override
    public boolean updateEmpleado(int numeroEmpleado, Empleado empleado) {
        for (int i = 0; i < empleados.size(); i++) {
            if (empleados.get(i).getNumeroEmpleado() == numeroEmpleado) {
                empleados.set(i, empleado);
                return true; // Empleado actualizado exitosamente
            }
        }
        return false; // Empleado no encontrado
    }

    @Override
    public boolean deleteEmpleado(int numeroEmpleado) {
        for (Empleado empleado : empleados) {
            if (empleado.getNumeroEmpleado() == numeroEmpleado) {
                empleados.remove(empleado);
                return true; // Empleado eliminado exitosamente
            }
        }
        return false; // Empleado no encontrado
    }
}
