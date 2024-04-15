package Persona.example.Persona.model;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class Persona {

    private String id;
    private LocalDateTime horaLlegada;
    private String placaVehiculo;


}
