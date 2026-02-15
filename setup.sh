#!/bin/bash

# Color para salida
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${YELLOW}════════════════════════════════════════${NC}"
echo -e "${YELLOW}  Setup: Registro de Visitantes${NC}"
echo -e "${YELLOW}════════════════════════════════════════${NC}\n"

# Verificar .NET SDK
echo -e "${YELLOW}📦 Verificando .NET SDK...${NC}"
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}❌ .NET SDK no está instalado${NC}"
    echo "Descárgalo desde: https://dotnet.microsoft.com/download"
    exit 1
fi

DOTNET_VERSION=$(dotnet --version)
echo -e "${GREEN}✓ .NET $DOTNET_VERSION encontrado${NC}\n"

# Verificar SQL Server
echo -e "${YELLOW}🗄️  Verificando SQL Server...${NC}"
if ! command -v sqlcmd &> /dev/null; then
    echo -e "${YELLOW}⚠️  sqlcmd no está instalado, omitiendo verificación${NC}"
    echo "Asegúrate de que SQL Server esté corriendo localmente"
else
    if sqlcmd -S localhost -U sa -P "$1" -Q "SELECT 1" &> /dev/null; then
        echo -e "${GREEN}✓ SQL Server conectado${NC}\n"
    else
        echo -e "${RED}❌ No se puede conectar a SQL Server${NC}"
        echo "Verifica las credenciales y que SQL Server esté corriendo"
        exit 1
    fi
fi

# Restaurar paquetes
echo -e "${YELLOW}📥 Restaurando paquetes NuGet...${NC}"
dotnet restore
if [ $? -ne 0 ]; then
    echo -e "${RED}❌ Error al restaurar paquetes${NC}"
    exit 1
fi
echo -e "${GREEN}✓ Paquetes restaurados${NC}\n"

# Compilar
echo -e "${YELLOW}🔨 Compilando el proyecto...${NC}"
dotnet build
if [ $? -ne 0 ]; then
    echo -e "${RED}❌ Error en la compilación${NC}"
    exit 1
fi
echo -e "${GREEN}✓ Compilación exitosa${NC}\n"

# Actualizar base de datos
echo -e "${YELLOW}🗄️  Aplicando migraciones a la base de datos...${NC}"
dotnet ef database update --verbose
if [ $? -ne 0 ]; then
    echo -e "${YELLOW}⚠️  Error al actualizar la BD (esto puede ser normal si ya existe)${NC}"
fi
echo -e "${GREEN}✓ Base de datos lista${NC}\n"

# Limpiar puerto si está en uso
echo -e "${YELLOW}🔌 Verificando puerto 5000...${NC}"
if lsof -i :5000 &> /dev/null; then
    echo -e "${YELLOW}⚠️  Puerto 5000 en uso, liberando...${NC}"
    sudo lsof -i :5000 | grep LISTEN | awk '{print $2}' | xargs kill -9 2>/dev/null
    sleep 2
fi

echo -e "${GREEN}✓ Puert 5000 disponible${NC}\n"

# Iniciar la aplicación
echo -e "${GREEN}════════════════════════════════════════${NC}"
echo -e "${GREEN}🚀 Iniciando la aplicación...${NC}"
echo -e "${GREEN}════════════════════════════════════════${NC}\n"

echo -e "${YELLOW}Abre tu navegador en: http://localhost:5000${NC}\n"

dotnet run --urls=http://localhost:5000
