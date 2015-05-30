﻿(function () {
    angular.module('app')

    .factory('factoryNombresApellidos', function () {

        return {
            listaNombres: function () {
                return ['Abelardo', 'Adela', 'Adiela', 'Adriana', 'Adriana Maria', 'Alba Lucia',
            'Albeiro', 'Alberto', 'Aldemar', 'Alejandra', 'Alejandro', 'Alexander', 'Alexandra',
            'Aleyda', 'Alfonso', 'Alfredo', 'Alicia', 'Alirio', 'Alonso', 'Alvaro', 'Amanda', 'Amparo',
            'Ana', 'Ana Cecilia', 'Ana Delia', 'Ana Isabel', 'Ana Lucia', 'Ana Maria', 'Ana Milena', 'Ana Rosa',
            'Ana Sofia', 'Ancizar', 'Anderson', 'Andrea', 'Andres', 'Andres Felipe', 'Andres Mauricio', 'Angel Maria',
            'Angela', 'Angela Maria', 'Angela Patricia', 'Angelica', 'Angelica Maria', 'Angie Paola', 'Antonio', 'Antonio Jose',
            'Aracelly', 'Armando', 'Arnulfo', 'Arturo', 'Aura Maria', 'Aurora', 'Beatriz', 'Beatriz Elena', 'Bebe', 'Bebe Por Nacer',
            'Benjamin', 'Bernardo', 'Bertha', 'Blanca', 'Blanca Cecilia', 'Blanca Ines', 'Blanca Nubia', 'Brayan', 'Brayan Stiven',
            'Camila', 'Camilo', 'Camilo Andres', 'Carlos', 'Carlos Alberto', 'Carlos Andres', 'Carlos Arturo', 'Carlos Eduardo', 'Carlos Enrique',
            'Carlos Julio', 'Carlos Mario', 'Carmen', 'Carmen Emilia', 'Carmen Rosa', 'Carmenza', 'Carolina', 'Catalina', 'Cecilia', 'Cesar',
            'Cesar Augusto', 'Clara Ines', 'Claudia', 'Claudia Lorena', 'Claudia Marcela', 'Claudia Milena', 'Claudia Patricia', 'Consuelo', 'Cristian',
            'Cristian Andres', 'Cristian Camilo', 'Cristian David', 'Cristina', 'Daniel', 'Daniel Felipe', 'Daniela', 'Danilo', 'Dario', 'David', 'Deyanira',
            'Diana', 'Diana Carolina', 'Diana Marcela', 'Diana Maria', 'Diana Milena', 'Diana Paola', 'Diana Patricia', 'Diego', 'Diego Alejandro',
            'Diego Alexander', 'Diego Andres', 'Diego Armando', 'Diego Fernando', 'Dioselina', 'Dora', 'Doralba', 'Doris', 'Edelmira',
            'Edgar', 'Edilberto', 'Edilma', 'Edison', 'Eduardo', 'Edwin', 'Efrain', 'Eliana', 'Elizabeth', 'Elvia', 'Elvira', 'Enrique',
            'Erika', 'Ernesto', 'Esperanza', 'Esteban', 'Estefania', 'Esther Julia', 'Fabian', 'Fabio', 'Fabio Nelson', 'Fabiola', 'Fanny',
            'Felipe', 'Fernando', 'Ferney', 'Flor Maria', 'Floralba', 'Francisco', 'Francisco Javier', 'Fredy', 'Gabriel', 'Gabriela',
            'Geraldine', 'Gerardo', 'German', 'Gilberto', 'Gildardo', 'Gilma', 'Giovanny', 'Gladis', 'Gladys', 'Gloria', 'Gloria Amparo',
            'Gloria Esperanza', 'Gloria Ines', 'Gloria Nancy', 'Gloria Patricia', 'Gonzalo', 'Graciela', 'Guillermo', 'Gustavo', 'Gustavo Adolfo',
            'Hector', 'Hector Fabio', 'Henry', 'Heriberto', 'Hernan', 'Hernando', 'Hugo', 'Humberto', 'Ines', 'Isabel', 'Isabel Cristina', 'Isabela',
            'Isabella', 'Ivan Dario', 'Jackeline', 'Jaime', 'Jaime Alberto', 'Jaime Andres', 'Jairo', 'Janeth', 'Javier', 'Jennifer', 'Jenny Paola',
            'Jesus', 'Jesus Antonio', 'Jesus David', 'Jesus Maria', 'Jhon Alejandro', 'Jhon Alexander', 'Jhon Edison', 'Jhon Freddy', 'Jhon Fredy',
            'Jhon Jairo', 'Jhon James', 'Jhon Sebastian', 'Jhonatan', 'Johan Sebastian', 'Johana', 'John Jairo', 'Jonathan', 'Jorge', 'Jorge Alberto',
            'Jorge Andres', 'Jorge Eduardo', 'Jorge Eliecer', 'Jorge Enrique', 'Jorge Hernan', 'Jorge Ivan', 'Jorge Luis', 'Jose', 'Jose Albeiro', 'Jose Alberto',
            'Jose Alejandro', 'Jose Alexander', 'Jose Alirio', 'Jose Antonio', 'Jose Daniel', 'Jose David', 'Jose Fernando', 'Jose Ignacio', 'Jose Jesus',
            'Jose Luis', 'Jose Manuel', 'Jose Miguel', 'Jose Omar', 'Jose Orlando', 'Jose Vicente', 'Josefina', 'Juan', 'Juan Andres', 'Juan Camilo', 'Juan Carlos',
            'Juan David', 'Juan De Dios', 'Juan De Jesus', 'Juan Diego', 'Juan Esteban', 'Juan Felipe', 'Juan Gabriel', 'Juan Jose', 'Juan Manuel', 'Juan Pablo',
            'Juan Sebastian', 'Julian', 'Julian Andres', 'Julian David', 'Juliana', 'Julio', 'Julio Cesar', 'Katerine', 'Katherine', 'Laura', 'Laura Camila', 'Laura Daniela',
            'Laura Sofia', 'Laura Valentina', 'Leidy', 'Leidy Jhoana', 'Leidy Johana', 'Leidy Tatiana', 'Leidy Viviana', 'Leonardo', 'Leonel', 'Leonor', 'Leticia', 'Libardo',
            'Libia', 'Ligia', 'Lilia', 'Liliana', 'Liliana Patricia', 'Lina Marcela', 'Lina Maria', 'Lorena', 'Lucelly', 'Lucero', 'Lucia', 'Lucila', 'Luis', 'Luis Alberto',
            'Luis Alejandro', 'Luis Alfonso', 'Luis Alfredo', 'Luis Angel', 'Luis Antonio', 'Luis Carlos', 'Luis Eduardo', 'Luis Enrique', 'Luis Evelio', 'Luis Felipe', 'Luis Fernando',
            'Luis Gonzaga', 'Luis Hernando', 'Luis Miguel', 'Luisa Fernanda', 'Luisa Maria', 'Luz Adriana', 'Luz Amparo', 'Luz Angela', 'Luz Dary', 'Luz Elena', 'Luz Estella',
            'Luz Helena', 'Luz Maria', 'Luz Marina', 'Luz Mary', 'Luz Mery', 'Luz Mila', 'Luz Miriam', 'Luz Stella', 'Magnolia', 'Maira Alejandra', 'Manuel', 'Manuel Antonio',
            'Manuela', 'Marcela', 'Marco Antonio', 'Marco Aurelio', 'Marco Tulio', 'Margarita', 'Maria', 'Maria Alejandra', 'Maria Amparo', 'Maria Angelica', 'Maria Antonia',
            'Maria Aurora', 'Maria Camila', 'Maria Cecilia', 'Maria Consuelo', 'Maria Cristina', 'Maria De Jesus', 'Maria De Los Angeles', 'Maria Del Carmen', 'Maria Del Pilar',
            'Maria Del Rosario', 'Maria Del Socorro', 'Maria Dolores', 'Maria Edilma', 'Maria Elena', 'Maria Elvia', 'Maria Esperanza', 'Maria Eugenia', 'Maria Fabiola',
            'Maria Fernanda', 'Maria Gladys', 'Maria Helena', 'Maria Ines', 'Maria Isabel', 'Maria Jose', 'Maria Ligia', 'Maria Lilia', 'Maria Luisa', 'Maria Mercedes',
            'Maria Nancy', 'Maria Nelly', 'Maria Ofelia', 'Maria Olga', 'Maria Oliva', 'Maria Paula', 'Maria Rosalba', 'Maria Rubiela', 'Maria Teresa', 'Maria Victoria',
            'Maria Yolanda', 'Mariana', 'Maribel', 'Maricela', 'Mariela', 'Mariluz', 'Marina', 'Marino', 'Mario', 'Marisol', 'Maritza', 'Marleny', 'Martha', 'Martha Cecilia',
            'Martha Isabel', 'Martha Liliana', 'Martha Lucia', 'Mary Luz', 'Mateo', 'Mauricio', 'Melva', 'Mercedes', 'Mery', 'Miguel', 'Miguel Angel', 'Miguel Antonio',
            'Miriam', 'Miryam', 'Monica', 'Monica Andrea', 'Nancy', 'Natalia', 'Nataly', 'Nelly', 'Nelson', 'Nestor', 'Nicolas', 'Nidia', 'Norberto', 'Nubia', 'Octavio',
            'Ofelia', 'Olga', 'Olga Lucia', 'Olga Patricia', 'Oliva', 'Omaira', 'Omar', 'Orlando', 'Oscar', 'Oscar Andres', 'Oscar Eduardo', 'Oscar Ivan', 'Pablo Emilio',
            'Paola', 'Paola Andrea', 'Patricia', 'Paula Andrea', 'Pedro', 'Pedro Antonio', 'Pedro Nel', 'Pedro Pablo', 'Rafael', 'Rafael Antonio', 'Ramiro', 'Raul', 'Reinaldo',
            'Ricardo', 'Rigoberto', 'Roberto', 'Robinson', 'Rocio', 'Rodrigo', 'Rogelio', 'Rosa', 'Rosa Elena', 'Rosa Maria', 'Rosalba', 'Ruben Dario', 'Rubiela', 'Ruby', 'Salome',
            'Samuel', 'Sandra', 'Sandra Liliana', 'Sandra Milena', 'Sandra Patricia', 'Sandra Viviana', 'Santiago', 'Sara', 'Saul', 'Sebastian', 'Sergio', 'Sergio Andres', 'Silvio',
            'Sofia', 'Soledad', 'Sonia', 'Stella', 'Susana', 'Tatiana', 'Teresa', 'Teresa De Jesus', 'Uriel', 'Valentina', 'Valeria', 'Vanessa', 'Veronica', 'Victor Alfonso', 'Victor Hugo',
            'Victor Manuel', 'Viviana', 'William', 'Wilmar', 'Wilson', 'Ximena', 'Yamile', 'Yaneth', 'Yeison', 'Yenifer', 'Yolanda', 'Yuliana'];
            },

            listaApellidos: function () {
                return ['Abril', 'Acero', 'Acevedo', 'Acosta', 'Agudelo', 'Aguilar', 'Aguilera', 'Aguirre', 'Alarcon', 'Aldana', 'Alfonso', 'Alonso', 'Alvarado', 'Alvaran',
                    'Alvarez', 'Alzate', 'Amariles', 'Amaya', 'Andica', 'Andrade', 'Angarita', 'Angel', 'Angulo', 'Aponte', 'Arango', 'Aranzazu', 'Arbelaez', 'Arboleda', 'Arce',
                    'Arcila', 'Ardila', 'Arenas', 'Arevalo', 'Arias', 'Aricapa', 'Aristizabal', 'Ariza', 'Arredondo', 'Arroyave', 'Arteaga', 'Atehortua', 'Avendaño', 'Avila', 'Ayala',
                    'Ballesteros', 'Bañol', 'Baquero', 'Barbosa', 'Barco', 'Baron', 'Barragan', 'Barrera', 'Barrero', 'Barreto', 'Barrios', 'Bautista', 'Becerra', 'Bedoya', 'Bejarano',
                    'Bello', 'Beltran', 'Benavides', 'Benavidez', 'Benitez', 'Benjumea', 'Bermudez', 'Bernal', 'Berrio', 'Betancourt', 'Betancourth', 'Betancur', 'Betancurt', 'Betancurth',
                    'Blanco', 'Blandon', 'Bocanegra', 'Bohorquez', 'Bolaños', 'Bolivar', 'Bonilla', 'Botero', 'Bravo', 'Bueno', 'Buitrago', 'Burgos', 'Buritica', 'Bustamante', 'Bustos',
                    'Caballero', 'Cabrera', 'Caceres', 'Cadavid', 'Cadena', 'Caicedo', 'Calderon', 'Calle', 'Calvo', 'Camacho', 'Camargo', 'Campo', 'Campos', 'Campuzano', 'Candamil',
                    'Cano', 'Canon', 'Cañas', 'Cañon', 'Cardenas', 'Cardona', 'Cardozo', 'Carmona', 'Caro', 'Carrillo', 'Carvajal', 'Casallas', 'Casas', 'Casta?O', 'Castaneda', 'Castano',
                    'Castañeda', 'Castaño', 'Castaqeda', 'Castellanos', 'Castiblanco', 'Castillo', 'Castrillon', 'Castro', 'Cataño', 'Ceballos', 'Celis', 'Cely', 'Cespedes', 'Chacon',
                    'Chaparro', 'Chavarro', 'Chavez', 'Chica', 'Cifuentes', 'Ciro', 'Clavijo', 'Colorado', 'Contreras', 'Cordoba', 'Corrales', 'Correa', 'Corredor', 'Cortes', 'Cortez',
                    'Cristancho', 'Cruz', 'Cuartas', 'Cubides', 'Cubillos', 'Cuellar', 'Cuervo', 'Cuesta', 'Davila', 'Daza', 'Delgado', 'Devia', 'Diaz', 'Dominguez', 'Duarte', 'Duque',
                    'Duran', 'Echavarria', 'Echeverry', 'Enciso', 'Escobar', 'Escudero', 'Espinosa', 'Espitia', 'Estrada', 'Fajardo', 'Fernandez', 'Figueroa', 'Flores', 'Florez', 'Fonseca',
                    'Forero', 'Franco', 'Fuentes', 'Gaitan', 'Galeano', 'Galindo', 'Gallego', 'Gallo', 'Galvez', 'Galvis', 'Galviz', 'Gamboa', 'Gañan', 'Garavito', 'Garces', 'Garcia',
                    'Garzon', 'Gaviria', 'Gil', 'Giraldo', 'Gomez', 'Gonzales', 'Gonzalez', 'Gordillo', 'Grajales', 'Granada', 'Granados', 'Grisales', 'Guapacha', 'Guarin', 'Guerra',
                    'Guerrero', 'Guevara', 'Gutierrez', 'Guzman', 'Henao', 'Heredia', 'Hernandez', 'Herrera', 'Hidalgo', 'Hincapie', 'Holguin', 'Hoyos', 'Huertas', 'Hurtado', 'Ibarra',
                    'Idarraga', 'Isaza', 'Izquierdo', 'Jaimes', 'Jaramillo', 'Jimenez', 'Jurado', 'Ladino', 'Lancheros', 'Lara', 'Largo', 'Laverde', 'Leal', 'Leon', 'Linares', 'Lizarazo',
                    'Llano', 'Llanos', 'Loaiza', 'Londono', 'Londoño', 'Lopez', 'Lotero', 'Lozada', 'Lozano', 'Lugo', 'Luna', 'Machado', 'Macias', 'Mahecha', 'Maldonado', 'Mancera',
                    'Manrique', 'Marin', 'Marquez', 'Martin', 'Martinez', 'Marulanda', 'Mateus', 'Maya', 'Mayorga', 'Mazo', 'Medina', 'Mejia', 'Melo', 'Mendez', 'Mendieta', 'Mendoza',
                    'Meneses', 'Merchan', 'Mesa', 'Meza', 'Millan', 'Mina', 'Miranda', 'Mojica', 'Molano', 'Molina', 'Moncada', 'Mondragon', 'Monroy', 'Monsalve', 'Montaño', 'Montealegre',
                    'Montenegro', 'Montero', 'Montes', 'Montoya', 'Mora', 'Morales', 'Moreno', 'Mosquera', 'Motato', 'Moya', 'Munoz', 'Muñoz', 'Muqoz', 'Murcia', 'Murillo', 'Naranjo',
                    'Narvaez', 'Navarrete', 'Navarro', 'Neira', 'Nieto', 'Ninguno', 'Nino', 'Niño', 'Norena', 'Noreña', 'Novoa', 'Nuñez', 'Obando', 'Ocampo', 'Ochoa', 'Olarte', 'Olaya',
                    'Oliveros', 'Ordoñez', 'Orjuela', 'Orozco', 'Orrego', 'Ortega', 'Ortegon', 'Ortiz', 'Osorio', 'Ospina', 'Ossa', 'Otalvaro', 'Ovalle', 'Oviedo', 'Pabon', 'Pacheco',
                    'Pachon', 'Padilla', 'Paez', 'Palacio', 'Palacios', 'Palomino', 'Pardo', 'Pareja', 'Parra', 'Parrado', 'Patino', 'Patiño', 'Pedraza', 'Pelaez', 'Pena', 'Penagos',
                    'Peña', 'Peqa', 'Peralta', 'Perdomo', 'Perea', 'Perez', 'Perilla', 'Pescador', 'Piedrahita', 'Pineda', 'Pinilla', 'Pino', 'Pinto', 'Pinzon', 'Piñeros', 'Porras',
                    'Posada', 'Poveda', 'Prada', 'Prieto', 'Puentes', 'Puerta', 'Pulgarin', 'Pulido', 'Quevedo', 'Quiceno', 'Quintana', 'Quintero', 'Quiroga', 'Quiroz', 'Ramirez',
                    'Ramos', 'Rangel', 'Reina', 'Rendon', 'Rengifo', 'Restrepo', 'Rey', 'Reyes', 'Riaño', 'Rico', 'Rincon', 'Rios', 'Rivas', 'Rivera', 'Riveros', 'Roa', 'Robayo', 'Robledo',
                    'Rocha', 'Rodas', 'Rodriguez', 'Rojas', 'Roldan', 'Roman', 'Romero', 'Roncancio', 'Rondon', 'Rozo', 'Rubiano', 'Rubio', 'Rueda', 'Ruiz', 'Saavedra', 'Sabogal', 'Saenz',
                    'Salamanca', 'Salas', 'Salazar', 'Salcedo', 'Saldarriaga', 'Salgado', 'Salinas', 'Sanabria', 'Sanchez', 'Sandoval', 'Santa', 'Santamaria', 'Santana', 'Santos',
                    'Sarmiento', 'Segura', 'Sepulveda', 'Serna', 'Serrano', 'Serrato', 'Sierra', 'Silva', 'Solano', 'Soler', 'Sosa', 'Soto', 'Suarez', 'Suaza', 'Tabares', 'Taborda',
                    'Tamayo', 'Tangarife', 'Tapasco', 'Tapias', 'Tejada', 'Tellez', 'Tique', 'Tobon', 'Toro', 'Torres', 'Tovar', 'Trejos', 'Triana', 'Trujillo', 'Uribe', 'Urrea', 'Urrego',
                    'Usma', 'Valbuena', 'Valderrama', 'Valencia', 'Valero', 'Vallejo', 'Vanegas', 'Varela', 'Vargas', 'Varon', 'Vasco', 'Vasquez', 'Vega', 'Velandia', 'Velasco', 'Velasquez',
                    'Velez', 'Vera', 'Vergara', 'Vidal', 'Villa', 'Villada', 'Villalobos', 'Villamil', 'Villegas', 'Vinasco', 'Yepes', 'Zambrano', 'Zamora', 'Zapata', 'Zarate', 'Zuleta', 'Zuluaga'];
            }
        }

    })

})();
