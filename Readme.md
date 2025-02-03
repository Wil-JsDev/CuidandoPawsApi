
<div align="center"><i>Well-being and care for each pet, in one place</i></div>

<hr />

# Cuidando Paws 🐾

The **Pet Care Platform** is a comprehensive solution designed to provide the best support in pet care.
We connect pet owners with a network of professional services, resources and tools that promote the well-being and health of their companions.
## 📦 Technologies

- `Asp.Net Core Web Api`
- `PostgreSQL`
- `Docker`
 
## 🚀 Features

- Book pet sitting services online.
- Registration and management of pet sitters on the platform.
- Security and management of the platform by administrators.

# API Documentation (V1)

## Acoount Endpoints 

## Base URL
`/api/v1/account`
### Register Caregiver

**URL:** `register-caregiver`  
**Method:** `POST`  
**Authorization:** Admin  
**Rate Limiting:** Disabled  
**Request Body:**
```json
{
    "firstName": "string",
    "lastName": "string",
    "username": "string",
    "phoneNumber": "string",
    "email": "string",
    "password": "string"
}
```
**Responses:**
- `200 OK`: Returns registered caregiver data.
- `400 Bad Request`: Validation errors.

---

### Register Pet Owner

**URL:** `/register-pet-owner`  
**Method:** `POST`  
**Authorization:** Admin  
**Rate Limiting:** Disabled  
**Request Body:** 
```json
{
    "firstName": "string",
    "lastName": "string",
    "username": "string",
    "phoneNumber": "string",
    "email": "string",
    "password": "string"
}
``` 
**Responses:**
- `200 OK`: Returns registered pet owner data.
- `400 Bad Request`: Validation errors.

---

### Register Admin

**URL:** `/register-admin`  
**Method:** `POST`  
**Authorization:** Admin  
**Rate Limiting:** Disabled  
**Request Body:** 
```json
{
    "firstName": "string",
    "lastName": "string",
    "username": "string",
    "phoneNumber": "string",
    "email": "string",
    "password": "string"
}
``` 
**Responses:**
- `200 OK`: Returns registered admin data.
- `400 Bad Request`: Validation errors.

---

### Confirm Account

**URL:** `/confirm-account`  
**Method:** `GET`  
**Rate Limiting:** Enabled (`fixed`)  
**Query Parameters:**
- `userId`: `string`
- `token`: `string`

**Responses:**
- `200 OK`: Account confirmed successfully.
- `404 Not Found`: Invalid token or user.

---

### Authenticate

**URL:** `/authenticate`  
**Method:** `POST`  
**Rate Limiting:** Enabled (`token`)  
**Request Body:**
```json
{
    "email": "string",
    "password": "string"
}
```
**Responses:**
- `200 OK`: Returns `AuthenticateResponse`.
- `400 Bad Request`: Account not confirmed.
- `401 Unauthorized`: Invalid credentials.
- `404 Not Found`: Email not found.

---

### Forgot Password

**URL:** `/forgot-password`  
**Method:** `POST`  
**Authorization:** Required  
**Rate Limiting:** Enabled (`fixed`)  
**Request Body:**
```json
{
    "email": "string"
}
```
**Responses:**
- `200 OK`: Success message.
- `404 Not Found`: Email not found.

---

### Reset Password

**URL:** `/reset-password`  
**Method:** `POST`  
**Authorization:** Required  
**Rate Limiting:** Disabled  
**Request Body:**
```json
{
    "email": "string",
    "password": "string"
}
```
**Responses:**
- `200 OK`: Password reset successfully.
- `404 Not Found`: Invalid request.

---

### Get Account Details

**URL:** `/{userId}`  
**Method:** `GET`  
**Authorization:** Required  
**Rate Limiting:** Enabled (`fixed`)  
**Responses:**
- `200 OK`: Returns `AccountDto`.
- `404 Not Found`: User not found.

---

### Logout

**URL:** `/logout`  
**Method:** `POST`  
**Authorization:** Required  
**Rate Limiting:** Enabled (`fixed`)  
**Responses:**
- `200 OK`: User logged out.

---

### Update Account Details

**URL:** `/{userId}`  
**Method:** `PUT`  
**Authorization:** Admin  
**Rate Limiting:** Enabled (`fixed`)  
**Request Body:**
```json
{
    "firstName": "string",
    "lastName": "string",
    "phoneNumber": "string",
    "username": "string"
}
```
**Responses:**
- `200 OK`: Updated account details.
- `404 Not Found`: User not found.
 
---

## Appoinment Endpoints (V1)

### Create Appointment

**URL:** `/api/v1/appoinment`  
**Method:** `POST`  
**Authorization:** `PetOwner, Admin`  
**Rate Limiting:** Enabled (`fixed`)  
**Request Body:**
```json
{
    "notes": "string",
    "idServiceCatalog": 0
}
```
**Responses:**
- `200 OK`: Returns created appointment data.
- `400 Bad Request`: Validation errors.

---

### Get Appointment by ID

**URL:** `/api/v1/appoinment/{id}`  
**Method:** `GET`  
**Authorization:** Required  
**Rate Limiting:** Enabled (`fixed`)  
**Responses:**
- `200 OK`: Returns `AppoinmentDTos`.
- `404 Not Found`: Appointment not found.

---

### Get All Appointments

**URL:** `/api/v1/appoinment`  
**Method:** `GET`  
**Authorization:** Required  
**Rate Limiting:** Enabled (`fixed`)  
**Responses:**
- `200 OK`: Returns a list of appointments.

---

### Delete Appointment

**URL:** `/api/v1/appoinment/{appoinmentId}`  
**Method:** `DELETE`  
**Authorization:** `PetOwner, Admin`  
**Rate Limiting:** Enabled (`fixed`)  
**Responses:**
- `204 No Content`: Appointment deleted successfully.
- `404 Not Found`: Appointment not found.

---

### Update Appointment

**URL:** `/api/v1/appoinment/{appoinmentId}`  
**Method:** `PATCH`  
**Authorization:** `PetOwner, Admin`  
**Rate Limiting:** Disabled  
**Request Body:**
```json
{
    "notes": "string",
    "idServiceCatalog": 0
}
```
**Responses:**
- `200 OK`: Returns updated appointment data.
- `404 Not Found`: Appointment not found.

---

### Check Appointment Availability

**URL:** `/api/v1/appoinment/check-availability/service-catalog/{serviceId}`  
**Method:** `GET`  
**Authorization:** `PetOwner, Admin`  
**Rate Limiting:** Enabled (`fixed`)  
**Responses:**
- `200 OK`: Returns service availability.
- `404 Not Found`: Service not found.

---

### Get Availability for a Service

**URL:** `/api/v1/appoinment/availability-service/service-catalog/{serviceCatalogId}`  
**Method:** `GET`  
**Authorization:** `PetOwner, Admin`  
**Rate Limiting:** Enabled (`fixed`)  
**Responses:**
- `200 OK`: Returns availability data.
- `404 Not Found`: Service not found.

---

### Get Last Added Appointment

**URL:** `/api/v1/appoinment/last-added`  
**Method:** `GET`  
**Authorization:** Required  
**Rate Limiting:** Enabled (`fixed`)  
**Query Parameters:**
- `filterDate`: `Enum`

**Responses:**
- `200 OK`: Returns last added appointment.
- `400 Bad Request`: Invalid request.

## Medical Record Endpoints (V1)

### Get All Medical Records

**URL:** `/api/v1.0/medical-record`  
**Method:** `GET`  
**Authorization:** Required  
**Rate Limiting:** Enabled (`fixed`)  
**Responses:**
- `200 OK`: Returns a list of `MedicalRecordDTos`.

---

### Get Medical Record by ID

**URL:** `/api/v1.0/medical-record/{id}`  
**Method:** `GET`  
**Authorization:** Required  
**Rate Limiting:** Enabled (`fixed`)  
**Path Parameters:**
- `id`: `int` (Medical Record ID)

**Responses:**
- `200 OK`: Returns `MedicalRecordDTos`.
- `404 Not Found`: Medical record not found.

---

### Create Medical Record

**URL:** `/api/v1.0/medical-record`  
**Method:** `POST`  
**Authorization:** Roles (`Caregiver, Admin`)  
**Rate Limiting:** Enabled (`fixed`)  
**Request Body:**
```json
{
    "treatment": "string",
    "recommendations": "string",
    "idPet": "int"
}
```
**Responses:**
- `200 OK`: Returns created `MedicalRecordDTos`.
- `400 Bad Request`: Validation errors.
- `404 Not Found`: Related entity not found.

---

### Update Medical Record

**URL:** `/api/v1.0/medical-record/{id}`  
**Method:** `PUT`  
**Authorization:** Roles (`Admin`)  
**Rate Limiting:** Enabled (`fixed`)  
**Path Parameters:**
- `id`: `int` (Medical Record ID)

**Request Body:**
```json
{
    "treatment": "string",
    "recommendations": "string",
    "idPet": "int"
}
```
**Responses:**
- `200 OK`: Returns updated `MedicalRecordDTos`.
- `400 Bad Request`: Validation errors.
- `404 Not Found`: Medical record not found.

---

# Pets Endpoints (v1)

## Base URL
`/api/v1/pets`

### Create a Pet
**Endpoint:** `POST /api/v1/pets`

**Authorization:** Required (`User` role)

**Rate Limiting:** Disabled

**Request Body:**
```json
{
  "namePaws": "string",
  "bred": "string",
  "age": 0,
  "color": "string",
  "notesPets": "string",
  "gender": "string",
  "speciesId": 0
}
```

**Responses:**
- `200 OK` - Returns the created pet.
- `400 Bad Request` - Validation errors.

---

### Delete a Pet
**Endpoint:** `DELETE /api/v1/pets/{id}`

**Authorization:** Required (`Admin` role)

**Rate Limiting:** Disabled

**Responses:**
- `204 No Content` - Pet deleted successfully.
- `404 Not Found` - Pet not found.

---

### Get a Pet by ID
**Endpoint:** `GET /api/v1/pets/{id}`

**Authorization:** Required

**Rate Limiting:** Enabled (`fixed`)

**Responses:**
- `200 OK` - Returns the pet details.
- `404 Not Found` - Pet not found.

---

### Update a Pet
**Endpoint:** `PUT /api/v1/pets/{id}`

**Authorization:** Required (`Caregiver`, `Admin` roles)

**Rate Limiting:** Enabled (`fixed`)

**Request Body:**
```json
{
  "namePaws": "string",
  "bred": "string",
  "age": 0,
  "gender": "string",
  "speciesId": 0
}
```

**Responses:**
- `200 OK` - Returns the updated pet.
- `400 Bad Request` - Validation errors.
- `404 Not Found` - Pet not found.

---

### Get Pets Added Today
**Endpoint:** `GET /api/v1/pets/last-added-today`

**Authorization:** Required

**Rate Limiting:** Enabled (`fixed`)

**Responses:**
- `200 OK` - Returns a list of pets added today.

---

### Get Paginated Pets
**Endpoint:** `GET /api/v1/pets/pagination?pageNumber={page}&pageSize={size}`

**Authorization:** Required

**Rate Limiting:** Enabled (`fixed`)

**Responses:**
- `200 OK` - Returns paginated pet list.
- `400 Bad Request` - Invalid pagination parameters.

---

## Data Transfer Objects (DTOs)

### CreatePetsDTos
```json
{
  "namePaws": "string",
  "bred": "string",
  "age": 0,
  "color": "string",
  "notesPets": "string",
  "gender": "string",
  "speciesId": 0
}
```

### PetsDTos
```json
{
  "petsId": 0,
  "namePaws": "string",
  "bred": "string",
  "age": 0,
  "color": "string",
  "adoptionStatus": true,
  "notesPets": "string",
  "dateOfEntry": "2025-01-01T00:00:00Z",
  "gender": "string",
  "createdAt": "2025-01-01T00:00:00Z",
  "speciesId": 0
}
```

### UpdatePetsDTos
```json
{
  "namePaws": "string",
  "bred": "string",
  "age": 0,
  "gender": "string",
  "speciesId": 0
}
```
---
# Service Catalog API (v1)

## Base URL

`/api/v1/service-catalog`

## Endpoints

### Get All Service Catalog Entries
**GET** `/api/v1/service-catalog`

**Authorization:** Required

**Rate Limiting:** Enabled (`fixed`)

**Response:**
- `200 OK`: Returns a list of service catalog entries.

---

### Get Service Catalog Entry by ID
**GET** `/api/v1/service-catalog/{id}`

**Authorization:** Required

**Rate Limiting:** Enabled (`fixed`)

**Response:**
- `200 OK`: Returns the service catalog entry.
- `404 Not Found`: Entry does not exist.

---

### Create a New Service Catalog Entry
**POST** `/api/v1/service-catalog`

**Authorization:** Required (`Caregiver, Admin` roles)

**Rate Limiting:** Disabled

**Request Body:**
```json
{
  "nameService": "string",
  "descriptionService": "string",
  "price": "decimal",
  "type": "string",
  "duration": "int"
}
```

**Response:**
- `200 OK`: Returns the created service catalog entry.
- `400 Bad Request`: Validation errors.

---

### Update an Existing Service Catalog Entry
**PUT** `/api/v1/service-catalog/{id}`

**Authorization:** Required (`Admin` role)

**Rate Limiting:** Enabled (`fixed`)

**Request Body:**
```json
{
  "nameService": "string",
  "descriptionService": "string",
  "price": "decimal",
  "type": "string",
  "duration": "int"
}
```

**Response:**
- `200 OK`: Returns the updated service catalog entry.
- `400 Bad Request`: Validation errors.
- `404 Not Found`: Entry does not exist.

---

### Delete a Service Catalog Entry
**DELETE** `/api/v1/service-catalog/{id}`

**Authorization:** Required (`Admin` role)

**Rate Limiting:** Disabled

**Response:**
- `204 No Content`: Successfully deleted.
- `400 Bad Request`: Deletion failed.

---

## DTOs

### CreateServiceCatalogDTos

```json
{
  "NameService": "string",
  "DescriptionService": "string",
  "Price": "decimal",
  "Type": "string",
  "Duration": "int"
}
```

### ServiceCatalogDTos
```json
{
  "ServiceCatalogId": "int",
  "NameService": "string",
  "DescriptionService": "string",
  "Price": "decimal",
  "CreatedAt": "DateTime",
  "Type": "string",
  "IsAvaible": "bool",
  "Duration": "int"
}
```

### UpdateServiceCatalogDTos
```json
{
  "NameService": "string",
  "DescriptionService": "string",
  "Price": "decimal",
  "Type": "string",
  "Duration": "int"
}

```
# Species API (v1)

## Base URL

`/api/v1.0/species`

## Authentication
All endpoints require authentication via JWT. Some endpoints require specific roles (`Caregiver`, `Admin`).

## Endpoints

### 1. Get All Species
**GET** `/api/v1.0/species`
- **Authorization:** Required
- **Rate Limiting:** Enabled
- **Response Codes:**
  - `200 OK`: Returns a list of all species.

#### Example Response
```json
[
  {
    "speciesId": 1,
    "descriptionOfSpecies": "Mammal",
    "entryOfSpecies": "2024-02-01T12:00:00Z"
  }
]
```

---
### 2. Get Species by ID
**GET** `/api/v1.0/species/{id}`
- **Authorization:** Required
- **Rate Limiting:** Enabled
- **Path Parameters:**
  - `id` (int, required) - The ID of the species.
- **Response Codes:**
  - `200 OK`: Returns the species data.
  - `404 Not Found`: Species not found.

#### Example Response
```json
{
  "speciesId": 1,
  "descriptionOfSpecies": "Mammal",
  "entryOfSpecies": "2024-02-01T12:00:00Z"
}
```

---
### 3. Create a New Species
**POST** `/api/v1.0/species`
- **Authorization:** Required (Roles: `Caregiver`, `Admin`)
- **Rate Limiting:** Disabled
- **Request Body:**
  - `descriptionOfSpecies` (string, required)
- **Response Codes:**
  - `200 OK`: Successfully created.
  - `400 Bad Request`: Validation failed.

#### Example Request
```json
{
  "descriptionOfSpecies": "Reptile"
}
```

#### Example Response
```json
{
  "speciesId": 2,
  "descriptionOfSpecies": "Reptile",
  "entryOfSpecies": "2024-02-01T12:05:00Z"
}
```

---
### 4. Update a Species
**PATCH** `/api/v1.0/species/{id}`
- **Authorization:** Required (Roles: `Caregiver`, `Admin`)
- **Rate Limiting:** Enabled
- **Path Parameters:**
  - `id` (int, required) - The ID of the species.
- **Request Body:**
  - `descriptionOfSpecies` (string, required)
- **Response Codes:**
  - `200 OK`: Successfully updated.
  - `404 Not Found`: Species not found.
  - `400 Bad Request`: Validation failed.

#### Example Request
```json
{
  "descriptionOfSpecies": "Amphibian"
}
```

#### Example Response
```json
{
  "speciesId": 2,
  "descriptionOfSpecies": "Amphibian",
  "entryOfSpecies": "2024-02-01T12:05:00Z"
}
```

---
### 5. Delete a Species
**DELETE** `/api/v1.0/species/{id}`
- **Authorization:** Required (Role: `Admin`)
- **Rate Limiting:** Disabled
- **Path Parameters:**
  - `id` (int, required) - The ID of the species.
- **Response Codes:**
  - `204 No Content`: Successfully deleted.
  - `404 Not Found`: Species not found.

---
### 6. Get Last Added Species
**GET** `/api/v1.0/species/last-added`
- **Authorization:** Required
- **Rate Limiting:** Enabled
- **Response Codes:**
  - `200 OK`: Returns the most recently added species.

#### Example Response
```json
{
  "speciesId": 3,
  "descriptionOfSpecies": "Bird",
  "entryOfSpecies": "2024-02-02T10:30:00Z"
}
```

---
### 7. Get Species Ordered by ID
**GET** `/api/v1.0/species/order-by?sort={sort}&direction={direction}`
- **Authorization:** Required
- **Rate Limiting:** Enabled
- **Query Parameters:**
  - `sort` (string, required) - The field to sort by (e.g., `speciesId`).
  - `direction` (string, required) - Sorting order (`asc` or `desc`).
- **Response Codes:**
  - `200 OK`: Returns sorted species list.
  - `400 Bad Request`: Invalid parameters.

#### Example Request
```
GET /api/v1.0/species/order-by?sort=speciesId&direction=asc
```
