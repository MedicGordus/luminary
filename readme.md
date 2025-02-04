# Luminary

Luminary is a webservice designed for integrating data with various capabilities tailored for different data handling scenarios.

Note: This project is under development as of January 28, 2025. It is in a non-functional state.

## Capabilities

### Fiber
- **Type**: Event-driven listener
- **Function**: Returns requested data by calling another configured endpoint.
- **Use Case**: Immediate data retrieval based on events.

### Beacon
- **Type**: Timer-based data transfer
- **Function**: Collects and sends data based on configurable timer settings.
- **Use Case**: Scheduled data updates or periodic data syncing.

### Laser
- **Type**: Event-driven listener with guaranteed delivery
- **Function**: Buffers payloads to ensure delivery to one or more endpoints, even if the endpoint is temporarily unavailable. Persists data to manage downtime.
- **Use Case**: Critical data that must be delivered with assurance, especially in systems where data integrity during network issues is crucial.

### Mushroom
- **Type**: Event-driven buffer
- **Function**: Collects and persists data according to configuration. Allows for the collection of data as events occur.
- **Use Case**: Data aggregation over time or for systems requiring data backup or historical data storage.

## Terms Used in Luminary

### Pulse
- **Description**: JSON payload with a specific structure.
- **Contents**:
  - Authorization information
  - Request-related information
- **Purpose**: To authenticate and specify the data request.

### Photon
- **Description**: JSON payload with a specific structure.
- **Structure**: Contains the payload under a "payload" key.
- **Purpose**: Used for transferring live or up-to-date data.

### Prism
- **Description**: Custom defined object structure.
- **Purpose**: Mapping data from one format to another, facilitating data transformation.

### Spore
- **Description**: JSON payload with a specific structure.
- **Structure**: Contains the payload under a "payload" key.
- **Purpose**: Transferring stale or outdated information, often for historical data analysis or recovery purposes.