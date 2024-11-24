package calculator;

import in.webserver.calculatorClient.Calculator;
import in.webserver.calculatorClient.CalculatorSoap;
import java.util.Scanner;

public class CalculatorClientSoap {

    public static void main(String[] args) {
        int number1, number2;
        float result;

        System.out.println("Bienvenido a la Calculadora SOAP!");

        // Crear una instancia del servicio
        Calculator service = new Calculator();
        CalculatorSoap proxy = service.getCalculatorSoap(); 
        
        try (Scanner scanner = new Scanner(System.in)) {
            System.out.println("Calculo suma, resta, multiplicacion, division");
            System.out.println("Ingrese dos numeros: ");

            // Manejo de entrada con validación
            while (true) {
                try {
                    System.out.print("Ingrese el primer numero ->  ");
                    number1 = Integer.parseInt(scanner.nextLine());

                    System.out.print("Ingrese el segundo numero -> ");
                    number2 = Integer.parseInt(scanner.nextLine());
                    break; 
                } catch (NumberFormatException e) {
                    System.out.println("Entrada no valida. Por favor, ingrese enteros validos.");
                }
            }

            // Llamar a los metodos
            result = proxy.add(number1, number2);
            System.out.println("La suma es: " + result);
            result = proxy.subtract(number1, number2);
            System.out.println("La resta es: " + result);
            result = proxy.divide(number1, number2);
            System.out.println("La division es: " + result);
            result = proxy.multiply(number1, number2);
            System.out.println("La multiplicacion es: " + result);
                    
        } catch (Exception e) {
            System.err.println("Ocurrio un error al llamar al servicio web: " + e.getMessage());
        }
    }
}