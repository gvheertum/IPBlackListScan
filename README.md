# IPBlackListScan
Simple console application that allows scanning a set of IP addresses to see if they are on one or more IP blacklists.

Extend your own build by providing an IP file in the root of the project with the name ip.list. Add Ip's to this list in the following format IP; Name (optional)

Example:
```
127.0.0.1;Test Localhost
```

The default test set has a baracuda test IP, which should always result in a "blacklisted" result (and will confirm the service is up)
