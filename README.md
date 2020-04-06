# LaboratorioCifrado
Para ingresar la clave en Cesar debe ser la direccion /Cipher/Cesar
Y en el form data de post man ingresar primero el archivo a cifrar con nombre Files y abajo en forma text la clave con nombre Clave. La cual primero valida que sea una clave aceptable. 
En la tercer fila se debe ingresar el nombre para el nuevo archivo con nombre Nombre.
Para descifrar es la misma manera solo que con /Decipher/Cesar

Para ingresar la clave en ZigZag debe ser /Cipher/ZigZag
Y en el form data de post man ingresar primero el archivo a cifrar con nombre Files y abajo en forma text la clave unicamente numeros con nombre Niveles, esto se valida si lo acepta o no.
En la tercer fila se debe ingresar el nombre para el nuevo archivo con nombre Nombre.
Para descifrar es la misma manera solo que con /Decipher/ZigZag 

Para ingresar la clave en Espiral debe ser /Cipher/Espiral
Y en el form data de post man ingresar primero el archivo a cifrar con nombre Files y abajo en forma text el numero de filas con nombbre Filas unicamente numeros, esto se valida si lo acepta o no.
En la tercer fila se debe ingresar el nombre para el nuevo archivo con nombre Nombre.
Para descifrar es la misma manera solo que con /Decipher/Espiral

Si la clave es válida para cifrar con el método designado, pique a "send y download" y esto descragara el archivo donde el usuario desee. Si no se descarga pique unicamente en "send" para que muestre el status code de la api y el porque la clave no puede cifrar o descifrar el algoritmo.

Visual debe permitir Visual Api 3.0
Lab Final en Master.
