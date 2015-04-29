## Sms Owl C# Wrapper

This package is wrapper of Sms Owl REST API hosted at [https://smsowl.in](https://smsowl.in). Sms Owl provides transactional and promotional SMS Gateway services.

### Installing Sms Owl package

Use following command to install meteor package.

	$ Install-Package Mahoujas.SmsOwl.Client

### Configuring credentials

Credentials should be configured before sending SMS. Credential should be passed as constructor argument for SmsOwlClient constructor
	
	var smsOwl = new SmsOwlClient("YOUR-ACCOUNT-ID", "YOUR-API-KEY");


### Sending promotional SMS


##### sendPromotionalSms(senderId,to,message,smsType)

 - senderId: Sender Id registered and approved in Sms Owl portal.
 - to: Either single number with country code or list of phone numbers.
 - message: Message to be sent.
 - smsType: It can have either of two values `normal` or `flash`
	
	
	
		try
		{
		   var smsId = await smsOwl.SendPromotionalSmsAsync("TESTER", "+9189876543210", "Hello C#", SmsType.Flash);;
		   	//Process smsId if you need to
		}
		catch (SmsOwlException e)
		{
		    //Handle exception.
		}

Return value is Sms Id for single SMS and List of SMS ids for Bulk Sms


##### sendPromotionalSms(senderId,to,message)

Same as above but smsType defaults to `SmsType.Normal`

### Sending Transactional SMS

##### sendTransactionalSms(senderId,to,templateId,placeholderObject);

 - senderId: Sender Id registered and approved in Sms Owl portal.
 - to: Destination number with country prefix. Only single number can be specified.
 - templateId: Template Id of message. Only template message can be send via transactional route.
 - placeholderObject: Placeholder values.

Lets assume templateId of `39ec9de0efa8a48cb6e60ee5` with following template.

	Hello {customerName}, your invoice amount is Rs. {amount}.

-


	try
    {
        var smsId = smsOwl.SendTransactionalSmsAsync("TESTER", "+919876543210", "39ec9de0efa8a48cb6e60ee5",new { CustomerName = "Bob", Amount = 200 });
        //Process smsid if needed.
    }
    catch (SmsOwlException e)
    {
        //Handle exception
    }


Return value is Sms Id.