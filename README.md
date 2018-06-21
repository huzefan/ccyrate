# Currency Rate Conversion

## Usage
/api/currency/BaseCCY/TargetCCY
```
/api/currency/CAD/USD
```
## Assumptions 
Third party API provides new rates every 60 seconds

## Further improvements
Add validations and error handling\
Add unit tests\
Refactor audit into separate service\
Remove hard coded configurations and read from config file\
Use NoSql database for audit\
Make use of distribued cache in case of multiple instances

## Note
Replace API key before running the application